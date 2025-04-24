using System.Collections.Generic;
using System.Linq;
using TipeMath;
namespace TipeEngine
{
    public abstract class GameObject
    {
        public Rect rect { get; set; }
        internal readonly List<IComponent> components = [];

        public GameObject(Rect _rect)
        {
            rect = _rect;
        }

        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }

        public bool RemoveComponent(IComponent component)
        {
            return components.Remove(component);
        }

        public T? GetComponent<T>() where T : class, IComponent
        {
            return components.OfType<T>().FirstOrDefault();
        }

        internal void Update()
        {
            foreach (IComponent component in components)
            {
                component.Update();
            }

            OnUpdate();
        }

        protected virtual void OnUpdate() { }

        internal void Draw()
        {
            foreach (IComponent component in components)
            {
                component.Draw();
            }

            OnDraw();
        }

        protected virtual void OnDraw() { }

        internal void GUI()
        {
            foreach (IComponent component in components)
            {
                component.GUI();
            }

            OnGUI();
        }

        protected virtual void OnGUI() { }

        internal void Unload()
        {
            foreach (IComponent component in components)
            {
                component.Unload();
            }

            OnUnload();
        }

        protected virtual void OnUnload() { }

        public void Move(Vector2 delta)
        {
            rect = new Rect(rect.Position + delta, rect.Size);
        }
    }
}
