using SFML.Window;

namespace Pong {
    public class PlayerController : IBatController {
        private readonly Keyboard.Key _up;
        private readonly Keyboard.Key _down;

        public PlayerController(Keyboard.Key up, Keyboard.Key down) {
            _up = up;
            _down = down;
        }

        public void Update(Bat bat) {
            if (Keyboard.IsKeyPressed(_up)) {
                bat.Velocity = new Vector2f(0, 1);
            } else if (Keyboard.IsKeyPressed(_down)) {
                bat.Velocity = new Vector2f(0, -1);
            } else {
                bat.Velocity = new Vector2f(0, 0);
            }
        }
    }
}
