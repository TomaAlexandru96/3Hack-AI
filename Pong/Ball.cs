using SFML.Graphics;
using SFML.Window;

#pragma warning disable 169

namespace Pong {
    public class Ball : ICollideable, IEntity {
        private Vector2f _position;
        private Vector2f _size;
        private Image image;
        private Vector2f velocity;
        private float speed;
        private Game _game;
        private readonly CircleShape _shape;
        public Bat RecentlyCollided { get; set; }

        public Ball(Vector2f position, Vector2f size, Game game) {
            _position = position;
            _shape = new CircleShape(_size.X) {FillColor = Color.White};
            _size = size;
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

        public void Update() { }

        public void Render(RenderTarget target) {
            _shape.Position = _position;
            target.Draw(_shape);
        }
    }
}
