﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using SFML.Graphics;
using SFML.Window;

#pragma warning disable 169

namespace Pong {
    public class Bat : ICollideable, IEntity {
        public PVector2F Velocity = new PVector2F(0, 0);
        private const double MaxBounceAngle = (5 * Math.PI) / 16;
        private readonly RectangleShape _shape;
        private IBatController _controller;
        private PVector2F _position;
        private PVector2F _size;
        private float _speed;
        private Game _game;

        public Bat(Game game, IBatController controller, PVector2F position, PVector2F size, float speed) {
            _game = game;
            _controller = controller;
            _position = position;
            Console.WriteLine(position);
            _size = size;
            _shape = new RectangleShape(_size) {FillColor = Color.White};
            _speed = speed;
        }

        public bool CollidesWith(ICollideable other) {
            return GetBoundingBox().Intersects(other.GetBoundingBox());
        }

        public IntRect GetBoundingBox() {
            return new IntRect((int) _position.X, (int) _position.Y, (int) _size.X, (int) _size.Y);
        }

        public void MakeMove(float delta) {
            _position = new PVector2F(_position.X + (_speed * Velocity.X * delta),
                _position.Y + (_speed * Velocity.Y * delta));
        }

        public void Update(float delta) {
            _controller.Update(this);

            MakeMove(delta);

            if (_position.Y + _size.Y >= _game.height && Velocity.Y > 0) {
                _position = new PVector2F(_position.X, _game.height - _size.Y);
            } else if (_position.Y <= 0 && Velocity.Y < 0) {
                _position = new PVector2F(_position.X, 0);
            }

            _game.GetEntities<Ball>()
                .ForEach(ball => {
                        if (CollidesWith(ball) && ball.RecentlyCollided != this) {
                            ball.RecentlyCollided = this;

                            var relativeIntersection = (_position.Y + (_size.Y / 2)) - (ball.Position.Y + ball.Radius);
                            var normalised = relativeIntersection / (_size.Y / 2);
                            var bounceAngle = normalised * MaxBounceAngle;

                            var right = ball.Velocity.X > 0;

                            ball.Velocity = new PVector2F((float) Math.Cos(bounceAngle),
                                -(float) Math.Sin(bounceAngle));
                            if (right) ball.ChangeVelocityHorizontal();
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
