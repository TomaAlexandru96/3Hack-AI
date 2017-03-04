using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using SFML.Graphics;
using SFML.Window;

namespace Pong {
    public class Bat : ICollideable, IEntity {
        private IBatController _controller;
        private Vector2f _position;
        private Vector2f _size;
        private Image image;
        private Vector2f _velocity;
        private float _speed;
        private Game _game;

        public Bat(IBatController controller, Vector2f position, Vector2f size, Texture texture, Game game) {
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
            _position = new Vector2f(_position.X+(_speed*_velocity.X),_position.Y+(_speed*_velocity.Y));
        }



        public void Update() {
            MakeMove();
            if (_position.Y >= _game.height && _velocity.Y > 0) {
                _position = new Vector2f(_position.X,_game.height);
            }
            else if (_position.Y - _size.Y<= 0 && _velocity.Y < 0) {
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

        public void Render() {
            throw new System.NotImplementedException();
        }


    }
}
