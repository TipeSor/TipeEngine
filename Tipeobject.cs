using SFML.Graphics;
using SFML.System;

namespace TipeEngine
{
    public class TipeObject : ConvexShape
    {
        public bool enabled {get; private set; }

        private Vector2f[] points;
        public Vector2f Size;

        public TipeObject(Texture tex)
        {
            if (tex == null) throw new ArgumentNullException(nameof(tex), "Texture cannot be null");
            
            Texture = tex;
            Size = (Vector2f)Texture.Size;

            points = new Vector2f[]
            {
                new Vector2f( Size.X / 2f,  Size.Y / 2f),
                new Vector2f(-Size.X / 2f,  Size.Y / 2f),
                new Vector2f(-Size.X / 2f, -Size.Y / 2f),
                new Vector2f( Size.X / 2f, -Size.Y / 2f)
            };

            SetPointCount(4);

            for (uint i = 0; i < points.Length; i++)
            {
                SetPoint(i, points[i]);
            }
        }

        public string Info()
        {
            string message = $"Position: {Position}\n";
            message += $"Rotation: {Rotation}\n";
            message += $"Scale: {Scale}\n";

            for (uint i = 0; i < points.Length; i++)
            {
                message += $"Point {i}: {points[i]}\n";
            }

            return message;
        }

        public void SetActive(bool value)
        {
            enabled = value;
        }
    }
}
