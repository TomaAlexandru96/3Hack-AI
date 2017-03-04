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
        private Vector2f velocity;
        private float speed;
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
            _position = new Vector2f(_position.X+(speed*velocity.X),_position.Y+(speed*velocity.Y));
        }


        public void Update() {
            _game.GetEntities<Ball>()
                .ForEach(ball => {
                        if (CollidesWith(ball) && ball.RecentlyCollided != this) {
                            ball.ChangeVelocity();
                        }
                    }
                );
            if (_position.Y >= _game.height && velocity.Y > 0) {
                ChangeVelocity();
            }
            else if (_position.Y <= 0 && velocity.Y < 0) {
                ChangeVelocity();
            }
            MakeMove();
        }

        public void Render() {
            throw new System.NotImplementedException();
        }

        public void ChangeVelocity() {
            velocity = new Vector2f(-velocity.X, -velocity.Y);
        }

    }
}
