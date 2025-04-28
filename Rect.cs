using System;

namespace TipeEngine
{
    public struct Rect
    {
        public static bool operator ==(Rect a, Rect b)
        {
            return a.X == b.X && a.Y == b.Y && a.W == b.W && a.H == b.H;
        }

        public static bool operator !=(Rect a, Rect b)
        {
            return !(a == b);
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Rect other && this == other;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y, W, H);
        }

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

        public readonly bool Overlap(Rect other)
        {
            return !(X + W < other.X ||
                     X > other.X + other.W ||
                     Y + H < other.Y ||
                     Y > other.Y + other.H);
        }

        public readonly bool Contains(Vector2 point)
        {
            return point.X >= X && point.X <= X + W &&
                   point.Y >= Y && point.Y <= Y + H;
        }

        public override readonly string ToString()
        {
            return $"(x:{X}, y:{Y}, width:{W}, height:{H})";
        }

        public static implicit operator Raylib_cs.Rectangle(Rect r)
        {
            return new Raylib_cs.Rectangle(r.X, r.Y, r.W, r.H);
        }
    }
}
