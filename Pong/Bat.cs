using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using SFML.Graphics;
using SFML.Window;

#pragma warning disable 169

namespace Pong {
    public class Bat : ICollideable, IEntity {
        public Vector2f Velocity;
        private IBatController _controller;
        private Vector2f _position;
        private Vector2f _size;
        private RectangleShape _shape;
        private Image image;
        private float speed = 1;
        private Game _game;

        public Bat(IBatController controller, Vector2f position, Vector2f size, Game game) {
            _controller = controller;
            _position = position;
            _size = size;
            _shape = new RectangleShape(_size) {FillColor = Color.White};
            _game = game;
        }

        public bool CollidesWith(ICollideable other) {
            return GetBoundingBox().Intersects(other.GetBoundingBox());
        }

        public IntRect GetBoundingBox() {
            return new IntRect((int) _position.X, (int) _position.Y, (int) _size.X, (int) _size.Y);
        }

        public void MakeMove() {
            _position = new Vector2f(_position.X + (speed * Velocity.X), _position.Y + (speed * Velocity.Y));
        }



        public void Update() {
            _controller.Update(this);
            MakeMove();
            if (_position.Y >= _game.height && Velocity.Y > 0) {
                _position = new Vector2f(_position.X,_game.height);
            }
            else if (_position.Y - _size.Y<= 0 && Velocity.Y < 0) {
                _position = new Vector2f(_position.X,_size.Y);
            }
             _game.GetEntities<Ball>()
                .ForEach(ball => {
                        if (CollidesWith(ball) && ball.RecentlyCollided != this) {
                            ball.ChangeVelocityVerticle();
                        }
                    }
                );

        }

        public void Render(RenderTarget target) {
            _shape.Position = _position;
            target.Draw(_shape);
        }


    }
}
