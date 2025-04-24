using System;
namespace TipeMath
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
    }

    public struct Rect
    {
        private Vector2 center;
        private int v1;
        private int v2;

        public static Rect Zero => new(0, 0, 0, 0);

        public readonly Vector2 Center => new(X + (W / 2f), Y + (H / 2f));

        public Vector2 Position
        {
            readonly get => new(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        public Vector2 Size
        {
            readonly get => new(W, H);
            set
            {
                W = value.X;
                H = value.Y;
            }
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float W { get; set; }
        public float H { get; set; }

        public Rect(float x, float y, float w, float h) { X = x; Y = y; W = w; H = h; }
        public Rect(Vector2 position, Vector2 size) { X = position.X; Y = position.Y; W = size.X; H = size.Y; }

        public Rect(Vector2 center, int v1, int v2) : this()
        {
            this.center = center;
            this.v1 = v1;
            this.v2 = v2;
        }

        public readonly bool Overlap(Rect other)
        {
            return !(X + W < other.X ||
                     X > other.X + other.W ||
                     Y + H < other.Y ||
                     Y > other.Y + other.H);
        }

        public override readonly string ToString()
        {
            return $"(x:{X}, y:{Y}, width:{W}, height:{H})";
        }
    }
}
