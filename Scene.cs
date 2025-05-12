using System.Collections.Generic;

namespace TipeEngine
{
    public abstract class Scene
    {
        internal List<GameObject> Objects { get; } = [];

        public virtual string Name { get; set; }

        protected Scene()
        {
            Name = GetType().Name;
        }

        public void AddObject(GameObject obj)
        {
            Objects.Add(obj);
        }

        public bool RemoveObject(GameObject obj)
        {
            return Objects.Remove(obj);
        }

        public GameObject[] GetObjects()
        {
            return [.. Objects];
        }

        public virtual void Start() { }

        public virtual void Load() { }

        public virtual void InternalUpdate()
        {
            foreach (GameObject obj in Objects)
            {
                obj.InternalUpdate();
            }
            Update();
        }

        public virtual void Update() { }

        public virtual void InternalDraw()
        {
            foreach (GameObject obj in Objects)
            {
                obj.InternalDraw();
            }
            Draw();
        }

        public virtual void Draw() { }

        public virtual void InternalGUI()
        {
            foreach (GameObject obj in Objects)
            {
                obj.InternalGUI();
            }
            GUI();
        }

        public virtual void GUI() { }

        public virtual void InternalUnload()
        {
            foreach (GameObject obj in Objects)
            {
                obj.InternalUnload();
            }
            Objects.Clear();
            Unload();
        }

        public virtual void Unload() { }
    }
}
