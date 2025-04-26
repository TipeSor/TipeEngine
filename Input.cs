using Raylib_cs;

namespace TipeEngine
{
    public static class Input
    {
        public static bool GetKey(KeyboardKey key)
        {
            return Raylib.IsKeyDown(key);
        }

        public static bool GetKeyDown(KeyboardKey key)
        {
            return Raylib.IsKeyPressed(key);
        }

        public static bool GetKeyDownRepeat(KeyboardKey key)
        {
            return Raylib.IsKeyPressedRepeat(key);
        }

        public static bool GetKeyUp(KeyboardKey key)
        {
            return Raylib.IsKeyReleased(key);
        }

        public static bool GetKeyReleased(KeyboardKey key)
        {
            return Raylib.IsKeyUp(key);
        }

        public static Vector2 GetMousePosition()
        {
            return Raylib.GetMousePosition();
        }

        public static Vector2 GetMouseDelta()
        {
            return Raylib.GetMouseDelta();
        }

        public static float GetMouseWheelMove()
        {
            return Raylib.GetMouseWheelMove();
        }

        public static Vector2 GetMouseWheelMoveV()
        {
            return Raylib.GetMouseWheelMoveV();
        }
    }
}
