using System;
using SFML.Graphics;
using SFML.Window;

#pragma warning disable 169

namespace Pong {
    public class Ball : ICollideable, IEntity {
        public Bat RecentlyCollided { get; set; }

        public float EffectiveSpeed {
            get { return _speed * (1 + Math.Abs(Velocity.Y)); }
        }

        public PVector2F Position;
        public PVector2F Velocity = new PVector2F(1, 0);
        public readonly int Radius;
        private readonly Game _game;
        private readonly CircleShape _shape;
        private  float _speed;

        public Ball(Game game, PVector2F position, int radius, float speed) {
            Position = position;
            _shape = new CircleShape(radius) {FillColor = Color.White};
            Radius = radius;
            _game = game;
            _speed = speed;
        }

        public bool CollidesWith(ICollideable other) {
            return GetBoundingBox().Intersects(other.GetBoundingBox());
        }

        public IntRect GetBoundingBox() {
            return new IntRect((int) Position.X, (int) Position.Y, Radius * 2, Radius * 2);
        }

        public void MakeMove(float delta) {
            Position = new PVector2F(Position.X + (EffectiveSpeed * Velocity.X * delta),
                Position.Y + (EffectiveSpeed * Velocity.Y * delta));
        }

        public void ChangeVelocityVertical() {
            Velocity = new PVector2F(Velocity.X, -Velocity.Y);
        }

        public void ChangeVelocityHorizontal() {
            Velocity = new PVector2F(-Velocity.X, Velocity.Y);
        }

        public void Update(float delta) {
            MakeMove(delta);
            if (Position.X - Radius < 0) {
                _game.PlayerTwoScore++;
                Position = new PVector2F((float) _game.width / 2 - 1.5f, (float) _game.height / 2 - ((float) Radius / 2));
                Velocity = new PVector2F(1,0);
                _speed = 160;
                RecentlyCollided = null;
            }
            if (Position.X + Radius> _game.width) {
                _game.PlayerOneScore++;
                Position = new PVector2F((float) _game.width / 2 - 1.5f, (float) _game.height / 2 - ((float) Radius / 2));
                Velocity = new PVector2F(-1,0);
                _speed = 160;
                RecentlyCollided = null;
            }
            if (Position.Y + Radius >= _game.height && Velocity.Y > 0) {
                ChangeVelocityVertical();
                Position = new PVector2F(Position.X, _game.height - Radius);
            } else if (Position.Y - Radius <= 0 && Velocity.Y < 0) {
                ChangeVelocityVertical();
                Position = new PVector2F(Position.X, Radius);
            }
        }

        public void Render(RenderTarget target) {
            _shape.Position = Position;
            target.Draw(_shape);
        }
    }
}
