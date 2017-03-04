using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.Window;

namespace Pong {
    public class Game {
        public uint width => _window.Size.X;
        public uint height => _window.Size.Y;
        private RenderWindow _window;
        private List<IEntity> _entities;

        public void Start() {
            bool closed = false;

            _window = new RenderWindow(new VideoMode(1200, 800), "Pong");
            _window.Closed += (a, b) => closed = true;

            Texture playerTexture = new Texture("whitepixel.png");

            _entities = new List<IEntity> {
                new Ball(new Vector2f(100, 100), new Vector2f(3, 3), this),
                new Bat(new PlayerController(Keyboard.Key.Up, Keyboard.Key.Down), new Vector2f(10, 50),
                    new Vector2f(4, 20), this),
                new Bat(new PlayerController(Keyboard.Key.W, Keyboard.Key.S),
                    new Vector2f(700, 50),
                    new Vector2f(4, 20), this)
            };

            while (!closed) {
                _entities.ForEach(x => x.Update());
                _window.Clear();
                _entities.ForEach(x => x.Render(_window));
                _window.Display();
            }
        }

        public List<T> GetEntities<T>() {
            return _entities.OfType<T>().ToList();
        }

        public static void Main(string[] args) {
            new Game().Start();
        }
    }
}
