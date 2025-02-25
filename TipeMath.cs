using SFML.System;
namespace TipeEngine
{
    class TipeMath
    {
        static public Vector2f pitchPoint(Vector2f point, float pitch)
        {
            float x = point.X;
            float y = point.Y * MathF.Cos(pitch);
            float z = point.Y * MathF.Sin(pitch);

            return new Vector2f(
                x / (90 + z) * 90,
                y / (90 + z) * 90
            );
        }

        static public float Deg2Rad(float degree)
        {
            return degree * 0.01745329251f;
        }

        static public float Rad2Deg(float radian)
        {
            return radian * 57.2957795131f;
        }
    }
}
