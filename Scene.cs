using System.Collections.Generic;
using System.Threading.Tasks;

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

        public virtual void Start() { }

        public virtual void Load() { }

        internal virtual void Update()
        {
            /*
            foreach (GameObject obj in Objects)
            {
                obj.Update();
            }
            */
            _ = Parallel.ForEach(Objects, static obj => obj.Update());
            OnUpdate();
        }

        protected virtual void OnUpdate() { }

        internal virtual void Draw()
        {
            foreach (GameObject obj in Objects)
            {
                obj.Draw();
            }
            OnDraw();
        }

        protected virtual void OnDraw() { }

        internal virtual void GUI()
        {
            foreach (GameObject obj in Objects)
            {
                obj.GUI();
            }
            OnGUI();
        }

        protected virtual void OnGUI() { }

        internal virtual void Unload()
        {
            foreach (GameObject obj in Objects)
            {
                obj.Unload();
            }
            Objects.Clear();
            OnUnload();
        }

        protected virtual void OnUnload() { }
    }
}
