using SFML.Graphics;
using SFML.Window;

#pragma warning disable 169

namespace Pong {
    public class Ball : ICollideable, IEntity {
        private Vector2f _position;
        private int _radius;
        private Image _image;
        private Vector2f _velocity;
        private float _speed = 1;
        private Game _game;
        private readonly CircleShape _shape;
        public Bat RecentlyCollided { get; set; }

        public Ball(Vector2f position, int radius, Game game) {
            _position = position;
            _shape = new CircleShape(radius) {FillColor = Color.White};
            _radius = radius;
            _game = game;
        }

        public bool CollidesWith(ICollideable other) {
            return GetBoundingBox().Intersects(other.GetBoundingBox());
        }

        public IntRect GetBoundingBox() {
            return new IntRect((int) _position.X, (int) _position.Y, _radius * 2, _radius * 2);
        }

        public void MakeMove() {
            _position = new Vector2f(_position.X + (_speed * _velocity.X), _position.Y + (_speed * _velocity.Y));
        }

        public void ChangeVelocityVerticle() {
            _velocity = new Vector2f(_velocity.X, -_velocity.Y);
        }

        public void ChangeVelocityHorizon() {
            _velocity = new Vector2f(-_velocity.X, _velocity.Y);
        }

        public void Update() {
            MakeMove();
            if (_position.Y + _radius >= _game.height && _velocity.Y > 0) {
                ChangeVelocityVerticle();
                _position = new Vector2f(_position.X, _game.height - _radius);
            } else if (_position.Y - _radius <= 0 && _velocity.Y < 0) {
                ChangeVelocityVerticle();
                _position = new Vector2f(_position.X, _game.height + _radius);
            }
        }

        public void Render(RenderTarget target) {
            _shape.Position = _position;
            target.Draw(_shape);
        }
    }
}
