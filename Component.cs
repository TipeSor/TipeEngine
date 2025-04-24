namespace TipeEngine
{
    public interface IComponent
    {
        void Update() { }
        void Draw() { }
        void GUI() { }
        void Unload() { }
    }
}
