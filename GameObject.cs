using System;
using System.Collections.Generic;
namespace TipeEngine
{
    public abstract class GameObject
    {
        public Rect rect { get; set; }
        internal readonly Dictionary<Type, IComponent> components = [];

        public GameObject(Rect _rect)
        {
            rect = _rect;
        }

        public bool AddComponent(IComponent component)
        {
            if (components.ContainsKey(component.GetType()))
            {
                return false;
            }
            components[component.GetType()] = component;
            return true;
        }

        public bool RemoveComponent(IComponent component)
        {
            return components.Remove(component.GetType());
        }

        public T? GetComponent<T>() where T : class, IComponent
        {
            return components.TryGetValue(typeof(T), out IComponent? component) ? (T?)component : null;
        }

        public virtual void InternalUpdate()
        {
            foreach (IComponent component in components.Values)
            {
                component.Update();
            }

            Update();
        }

        public virtual void Update() { }

        public virtual void InternalDraw()
        {
            foreach (IComponent component in components.Values)
            {
                component.Draw();
            }

            Draw();
        }

        public virtual void Draw() { }

        public virtual void InternalGUI()
        {
            foreach (IComponent component in components.Values)
            {
                component.GUI();
            }

            GUI();
        }

        public virtual void GUI() { }

        public virtual void InternalUnload()
        {
            foreach (IComponent component in components.Values)
            {
                component.Unload();
            }

            Unload();
        }

        public virtual void Unload() { }

        public void Move(Vector2 delta)
        {
            rect = new Rect(rect.Position + delta, rect.Size);
        }
    }
}
