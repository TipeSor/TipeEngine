using Raylib_cs;

namespace TipeEngine
{
    public class RectRenderer(GameObject _gameObject, Color? _color = null) : IComponent
    {
        public GameObject gameObject { get; } = _gameObject;
        public Color color { get; set; } = _color ?? Color.White;

        public void Update() { }
        public void Draw()
        {
            Rect rect = gameObject.rect;
            Raylib.DrawRectangleRec(rect, color);
        }
    }

    public class EllipseRenderer(GameObject _gameObject, Color? _color = null) : IComponent
    {
        public GameObject gameObject { get; } = _gameObject;
        public Color color { get; set; } = _color ?? Color.White;

        public void Update() { }
        public void Draw()
        {
            Rect rect = gameObject.rect;
            Vector2 center = rect.Center;
            float h = rect.W / 2f;
            float v = rect.H / 2f;

            Raylib.DrawEllipse((int)center.X, (int)center.Y, h, v, color);
        }
    }

    public class PixelRenderer(GameObject _gameObject, Color? _color = null) : IComponent
    {
        public GameObject gameObject { get; } = _gameObject;
        public Color color { get; set; } = _color ?? Color.White;

        public void Update() { }
        public void Draw()
        {
            Rect rect = gameObject.rect;
            Raylib.DrawPixel((int)rect.X, (int)rect.Y, color);
        }
    }

    public class TextureRenderer(GameObject _gameObject, Texture2D _texture, Color? _tint = null) : IComponent
    {
        public GameObject gameObject { get; } = _gameObject;
        public Texture2D texture { get; } = _texture;
        public Color tint { get; set; } = _tint ?? Color.White;

        public void Draw()
        {
            Rect rect = gameObject.rect;
            Rectangle sourceRect = new(0, 0, texture.Width, -texture.Height);
            Rectangle destRect = new(rect.X, rect.Y, rect.W, rect.H);
            Vector2 origin = new(0, 0);

            Raylib.DrawTexturePro(texture, sourceRect, destRect, origin, 0f, tint);
        }

        public void Unload()
        {
            Raylib.UnloadTexture(texture);
        }
    }

    public class DebugRenderer(GameObject _gameObject) : IComponent
    {
        public GameObject gameObject { get; } = _gameObject;

        public void Draw()
        {
            if (!Game.Debug)
            {
                return;
            }
            Rect rect = gameObject.rect;
            Raylib.DrawRectangleLines((int)rect.X, (int)rect.Y, (int)rect.W, (int)rect.H, Color.Red);
        }
    }
}
