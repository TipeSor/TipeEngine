using System;
using Raylib_cs;

namespace TipeEngine
{
    public static class Game
    {
        public static Rect Screen { get; private set; }
        public static bool Debug { get; set; }

        public static event Action? OnGameStart;
        public static event Action? OnGameExit;

        public static void Run(Rect screen, ConfigFlags configFlags, int TargetFPS = 60)
        {
            Screen = screen;
            Raylib.SetConfigFlags(configFlags);
            Raylib.InitWindow((int)Screen.W, (int)Screen.H, "Game");
            Raylib.SetTargetFPS(TargetFPS);

            ResourceManager.CacheComponents();

            OnGameStart?.Invoke();

            SceneManager.LoadScene(0);
            Font font = Raylib.LoadFont("assets/Roboto-Regular.ttf");
            while (!Raylib.WindowShouldClose())
            {
                SceneManager.ActiveScene.InternalUpdate();

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                SceneManager.ActiveScene.InternalDraw();
                SceneManager.ActiveScene.InternalGUI();

                string data = $"FPS: {Raylib.GetFPS()}\nScene: {SceneManager.ActiveScene.Name}\nDebug: {Debug}\nObjects: {SceneManager.ActiveScene.Objects.Count}";
                Raylib.DrawTextEx(font, data, new Vector2(10, 10), 32, 1, Color.White);
                Raylib.EndDrawing();
            }
            Raylib.UnloadFont(font);
            OnGameExit?.Invoke();
            Raylib.CloseWindow();
        }
    }
}
