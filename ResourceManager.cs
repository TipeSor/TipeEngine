using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Raylib_cs;

namespace TipeEngine
{
    public static class ResourceManager
    {
        private static readonly Dictionary<string, object> loadedObjects = [];
        private static readonly Dictionary<string, ComponentContext> ComponentCache = [];

        private static readonly Dictionary<Type, Func<string, object>> CustomDeserializers = new()
        {
            { typeof(Texture2D), static path => LoadTexture(path) },
            { typeof(Image), static path => LoadImage(path) },
            { typeof(Sound), static path => LoadSound(path) },
            { typeof(GameObject), static _ => throw new NotSupportedException($"can't deserialize a nested GameObject") }
        };

        internal static void CacheComponents()
        {
            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(static a => a.GetTypes())
                .Where(static t => typeof(IComponent).IsAssignableFrom(t)))
            {
                string name = type.FullName ?? throw new UnreachableException("some how.... a type has no name...");

                ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

                if (constructors.Length == 0)
                {
                    continue;
                }

                ConstructorInfo constructor =
                    constructors.FirstOrDefault(static c => Attribute.IsDefined(c, typeof(PreferredConstructorAttribute))) ??
                    constructors.FirstOrDefault() ??
                    throw new UnreachableException("There are more than 0 contstructors yet it failed to get the first one");

                ParameterInfo[] parameters = constructor.GetParameters();

                ComponentContext context = new(type, constructor);
                ComponentCache[name] = context;
            }
        }

        public static T LoadGameObject<T>(string path, string variant = "") where T : GameObject
        {
            return (T)LoadGameObject(path, typeof(T), variant);
        }

        public static object LoadGameObject(string path, Type type, string variant = "")
        {

            path += string.IsNullOrEmpty(variant) ? $"{type.Name}.json" : $"{type.Name}_{variant}.json";
            string json = File.ReadAllText(path);
            JObject root = JObject.Parse(json);

            JToken objectToken = root["object"] ??
                throw new SerializationException($"Could not find `object` in `{path}`");

            object? gameObjectRaw = objectToken.ToObject(type);
            if (gameObjectRaw is not GameObject gameObject)
            {
                throw new SerializationException($"Failed to deserialize `object` from `{path}`");
            }

            JToken? componentsToken = root["components"];
            if (componentsToken == null)
            {
                return gameObject;
            }

            if (componentsToken is JArray componentsArray)
            {
                foreach (JToken componentToken in componentsArray)
                {
                    if (componentToken is not JObject componentObject)
                    {
                        continue;
                    }

                    IComponent component = LoadComponent(componentObject, gameObject);
                    _ = gameObject.AddComponent(component);
                }
            }

            return gameObject;
        }

        private static IComponent LoadComponent(JObject componentObject, GameObject? baseObject = null)
        {
            string componentTypeName = componentObject["type"]?.ToString() ??
                throw new SerializationException("failed to get component type");

            if (!ComponentCache.TryGetValue(componentTypeName, out ComponentContext? componentContext))
            {
                throw new SerializationException($"can't find component context of `{componentTypeName}`");
            }

            if (componentContext == null)
            {
                throw new UnreachableException($"Somehow {componentContext} is null... ¯\\_(ツ)_/¯");
            }

            ConstructorInfo constructor = componentContext.constructor;
            ParameterInfo[] parameters = componentContext.parameters;
            List<object> parameterList = [];
            foreach (ParameterInfo parameter in parameters)
            {
                Type parameterType = parameter.ParameterType;
                string parameterName = parameter.Name!;
                JToken parameterToken = componentObject[parameterName] ??
                    throw new SerializationException($"no `{parameterName}` on component `{componentTypeName}`");
                object parameterObject = parameterType.IsGenericType
                    ? parameterToken.ToObject(parameterType) ??
                        throw new SerializationException($"failed to deserialize `{parameterName}` from `{componentTypeName}`")
                    : ResolveParameter(parameterType, parameterToken, baseObject, parameterName, componentTypeName);
                parameterList.Add(parameterObject);
            }

            return (IComponent)constructor.Invoke([.. parameterList]);
        }

        private static object ResolveParameter(Type parameterType, JToken token, GameObject? baseObject, string paramName, string componentName)
        {
            string value = token.ToObject<string>() ??
                throw new SerializationException($"failed to deserialize `{paramName}`");

            return parameterType == typeof(GameObject) && value == "base"
                ? baseObject ?? throw new ArgumentNullException(nameof(baseObject), "Base object was not provided.")
                : CustomDeserializers.TryGetValue(parameterType, out Func<string, object>? loader)
                ? loader(value)
                : throw new SerializationException($"Unhandled type {parameterType} in `{componentName}`");
        }

        private static Sound LoadSound(string path)
        {
            return LoadWithCache(path, Raylib.LoadSound);
        }

        private static Image LoadImage(string path)
        {
            return LoadWithCache(path, Raylib.LoadImage);
        }

        private static Texture2D LoadTexture(string path)
        {
            return LoadWithCache(path, Raylib.LoadTexture);
        }

        private static T LoadWithCache<T>(string path, Func<string, T> loader)
        {
            if (loadedObjects.TryGetValue(path, out object? cache) && cache is T t)
            {
                return t;
            }
            T result = loader(path);
            loadedObjects[path] = result!;
            return result;
        }

        public static void ClearObjects()
        {
            foreach (object loadedObject in loadedObjects.Values)
            {
                if (loadedObject is Sound sound)
                {
                    Raylib.UnloadSound(sound);
                }
                else if (loadedObject is Image image)
                {
                    Raylib.UnloadImage(image);
                }
                else if (loadedObject is Texture2D texture)
                {
                    Raylib.UnloadTexture(texture);
                }
            }

            loadedObjects.Clear();
        }
    }

    public class ComponentContext
    {
        public Type type { get; }
        public ConstructorInfo constructor { get; }
        public ParameterInfo[] parameters { get; }

        public ComponentContext(Type _type, ConstructorInfo _constructor)
        {
            type = _type;
            constructor = _constructor;
            parameters = constructor.GetParameters();
        }
    }

    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public class PreferredConstructorAttribute : Attribute
    {
        public PreferredConstructorAttribute() { }
    }
}
