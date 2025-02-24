using SFML.Graphics;
using SFML.System;

namespace TipeEngine
{
    public class TipeObject : Drawable
    {
        Texture texture;
        ConvexShape shape;

        Vector2f[] points;
        Vector2f size;
        public Vector2f Size
        {
            get { return size; }
        }

        public Vector2f Position
        {
            get { return shape.Position; }
            set => shape.Position = value;
        }

        public Vector2f Scale
        {
            get { return shape.Scale; }
            set { shape.Scale = value; }
        }

        public float Rotation
        {
            get { return shape.Rotation; }
            set { shape.Rotation = value; }
        }

        public TipeObject(Texture tex)
        {

            if (tex == null) throw new Exception("texture is null");
            texture = tex;

            size = (Vector2f)texture.Size;

            points = new Vector2f[]{
                new Vector2f( size.X / 2f,  size.Y / 2f),
                new Vector2f(-size.X / 2f,  size.Y / 2f),
                new Vector2f(-size.X / 2f, -size.Y / 2f),
                new Vector2f( size.X / 2f, -size.Y / 2f)
            };

            shape = new ConvexShape(4);
            shape.Texture = tex;

            foreach (uint index in new uint[] { 0, 1, 2, 3 })
            {
                shape.SetPoint(index, points[index]);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(shape, states);
        }

        public string Info()
        {
            string message = $"Position: {Position}\n";
            message += $"Rotation: {0f}\n";
            message += $"Scale:    {Scale}\n";

            foreach (uint index in new uint[] { 0, 1, 2, 3 })
            {
                message += $"Point {index}: {points[index]}\n";
            }

            return message;
        }
    }
}
