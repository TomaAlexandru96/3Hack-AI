using System.Collections.Generic;
using System.IO;
using System.Linq;
using SFML.Graphics;
using SFML.Window;

namespace Pong {
    public class Game {
        private Window _window;
        private List<IEntity> _entities;

        public void Start() {
            bool closed = false;

            _window = new RenderWindow(new VideoMode(1200, 800), "Pong");
            _window.Closed += (a, b) => closed = true;

            Texture playerTexture = new Texture("whitepixel.png");

            _entities = new List<IEntity> {
                new Ball(2),
                new Bat(new PlayerController(Keyboard.Key.Up, Keyboard.Key.Down), new Vector2i(10, 50),
                    new Vector2i(4, 20), playerTexture),
                new Bat(new PlayerController(Keyboard.Key.W, Keyboard.Key.S),
                    new Vector2i(700, 50),
                    new Vector2i(4, 20), playerTexture)
            };

            while (!closed) {
                _entities.ForEach(x => x.Update());
                _entities.ForEach(x => x.Render());
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
