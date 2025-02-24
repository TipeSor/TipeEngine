using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace TipeEngine
{
    class Program
    {
        static List<TipeObject> objects =  new List<TipeObject>();

        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "TipeEngine");

            window.Closed += (sender, e) => window.Close();

            Texture texture = new Texture("Images/card-back.png");

            objects.Add(new TipeObject(texture)
            {
                Position = new Vector2f(400f, 300f),
                Scale = new Vector2f(2f, 2f)
            });

            objects.Add(new TipeObject(texture)
            {
                Position = new Vector2f(300f, 300f),
                Scale = new Vector2f(2f, 2f)
            });

            objects.Add(new TipeObject(texture)
            {
                Position = new Vector2f(500f, 300f),
                Scale = new Vector2f(2f, 2f)
            });

            Console.WriteLine(objects[0].Info());

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(new Color(10, 10, 10, 255));

                objects.ForEach(o => window.Draw(o));

                window.Display();
            }
        }
    }
}
