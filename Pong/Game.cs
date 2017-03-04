using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace Pong {
    public class Game {
        private Window _window;
        private List<IEntity> _entities;

        public void Start() {
            _window = new RenderWindow(new VideoMode(1200, 800), "Pong");
            _entities = new List<IEntity>();

            while (true) {
                _entities.ForEach(x => x.Update());
                _entities.ForEach(x => x.Render());
            }
        }

        public static void Main(string[] args) {
            new Game().Start();
        }
    }
}
