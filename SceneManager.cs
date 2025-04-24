using System;
using System.Collections.Generic;

namespace TipeEngine
{
    public static class SceneManager
    {
        public static Scene ActiveScene { get; private set; } = null!;
        public static List<Func<Scene>> Scenes { get; } = [];
        public static int SceneIndex { get; private set; } = -1;

        public static event Action<Scene?, Scene>? OnSceneLoaded;

        public static void Next()
        {
            LoadScene((SceneIndex + 1) % Scenes.Count);
        }

        public static void LoadScene(int sceneIndex)
        {
            if (Scenes.Count == 0)
            {
                throw new InvalidOperationException("Scene count can't be 0");
            }

            if (sceneIndex < 0 || sceneIndex >= Scenes.Count)
            {
                return;
            }

            Scene OldScene = ActiveScene;
            Scene NewScene = Scenes[sceneIndex]();

            ActiveScene = NewScene;
            OldScene?.Unload();

            NewScene.Load();
            NewScene.Start();

            OnSceneLoaded?.Invoke(OldScene, NewScene);

            SceneIndex = sceneIndex;
        }
    }
}
