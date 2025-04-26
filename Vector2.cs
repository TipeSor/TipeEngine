using System;

namespace TipeEngine
{
    public struct Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2 operator *(Vector2 a, float scalar)
        {
            return new(a.X * scalar, a.Y * scalar);
        }
        public static Vector2 operator /(Vector2 a, float scalar)
        {
            return new(a.X / scalar, a.Y / scalar);
        }

        public static Vector2 Abs(Vector2 a)
        {
            return new Vector2(MathF.Abs(a.X), MathF.Abs(a.Y));
        }

        public readonly float Length()
        {
            return MathF.Sqrt((X * X) + (Y * Y));
        }

        public readonly float LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

        public readonly Vector2 Normalized()
        {
            float len = Length();
            return len == 0 ? new(0, 0) : this / len;
        }

        public readonly float Dot(Vector2 other)
        {
            return (X * other.X) + (Y * other.Y);
        }

        public override readonly string ToString()
        {
            return $"({X}, {Y})";
        }

        public static implicit operator System.Numerics.Vector2(Vector2 v)
        {
            return new System.Numerics.Vector2(v.X, v.Y);
        }

        public static implicit operator Vector2(System.Numerics.Vector2 v)
        {
            return new Vector2(v.X, v.Y);
        }
    }
}
