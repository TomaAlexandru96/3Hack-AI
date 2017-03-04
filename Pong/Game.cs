using SFML.Graphics;
using SFML.Window;

namespace Pong {
    public class Game {
        private SFML.Window.Window window;

        public void Start() {
            window = new RenderWindow(new VideoMode(1200, 800), "Pong");

            while (true) {
                
            }
        }

        public static void Main(string[] args) {
            new Game().Start();
        }
    }
}
