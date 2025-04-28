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

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Vector2 other && this == other;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }


        public static Vector2 Zero => new(0, 0);
        public static Vector2 One => new(1, 1);
        public static Vector2 Up => new(0, -1);
        public static Vector2 Down => new(0, 1);
        public static Vector2 Left => new(-1, 0);
        public static Vector2 Right => new(1, 0);


        public static Vector2 Abs(Vector2 a)
        {
            return new Vector2(MathF.Abs(a.X), MathF.Abs(a.Y));
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            return (a - b).Length();
        }

        public static bool Approximately(Vector2 a, Vector2 b, float epsilon = 0.0001f)
        {
            return (a - b).LengthSquared() < epsilon * epsilon;
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
