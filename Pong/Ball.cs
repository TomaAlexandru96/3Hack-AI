using SFML.Graphics;
using SFML.Window;

namespace Pong {
    public class Ball : ICollideable, IEntity {
        private IBatController _controller;
        private Vector2f _position;
        private Vector2f _size;
        private Image image;
        private Vector2f velocity;
        private float speed;
        private Game _game;
        public Bat RecentlyCollided { get; set; }

        public Ball(IBatController controller, Vector2f position, Vector2f size, Texture texture, Game game) {
            _controller = controller;
            _position = position;
            _size = size;
            Sprite bat = new Sprite(texture);
            _game = game;
        }

        public bool CollidesWith(ICollideable other) {
            return GetBoundingBox().Intersects(other.GetBoundingBox());
        }

        public IntRect GetBoundingBox() {
            return new IntRect((int) _position.X, (int) _position.Y, (int) _size.X, (int) _size.Y);
        }

        public void MakeMove() {
            throw new System.NotImplementedException();
        }

        public void ChangeVelocity() {
            throw new System.NotImplementedException();
        }

        public void Update() {
            throw new System.NotImplementedException();
        }

        public void Render() {
            throw new System.NotImplementedException();
        }

    }
}
