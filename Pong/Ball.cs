using SFML.Graphics;
using SFML.Window;

namespace Pong {
    public class Ball : ICollideable, IEntity {
        private IBatController _controller;
        private Vector2f _position;
        private int _radius;
        private Image _image;
        private Vector2f _velocity;
        private float _speed;
        private Game _game;
        public Bat RecentlyCollided { get; set; }

        public Ball(IBatController controller, Vector2f position, int radius, Texture texture, Game game) {
            _controller = controller;
            _position = position;
            _radius = radius;
            Sprite bat = new Sprite(texture);
            _game = game;
        }

        public bool CollidesWith(ICollideable other) {
            return GetBoundingBox().Intersects(other.GetBoundingBox());
        }

        public IntRect GetBoundingBox() {
            return new IntRect((int) _position.X, (int) _position.Y, _radius*2,_radius*2);
        }

        public void MakeMove() {
            _position = new Vector2f(_position.X+(_speed*_velocity.X),_position.Y+(_speed*_velocity.Y));
        }



        public void ChangeVelocityVerticle() {
            _velocity = new Vector2f(_velocity.X,-_velocity.Y);
        }

        public void ChangeVelocityHorizon() {
            _velocity = new Vector2f(-_velocity.X,_velocity.Y);
        }

        public void Update() {

            MakeMove();
            if (_position.Y + _radius >= _game.height && _velocity.Y > 0) {
                ChangeVelocityVerticle();
                _position = new Vector2f(_position.X,_game.height-_radius);
            }
            else if (_position.Y - _radius <= 0 && _velocity.Y < 0) {
                ChangeVelocityVerticle();
                _position = new Vector2f(_position.X,_game.height+_radius);
            }

        }

        public void Render() {
            throw new System.NotImplementedException();
        }

        public Vector2f GetVelocity() {
            return _velocity;
        }

    }
}
