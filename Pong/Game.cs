using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pong {
    public class Game {
        public uint width {
            get { return _window.Size.X; }
        }

        public uint height {
            get { return _window.Size.Y; }
        }

        public int PlayerOneScore {
            get { return _playerOneScore; }
            set {
                _playerOneScore = value;
                _playerOneText.DisplayedString = String.Format("Player One {0}", _playerOneScore);
            }
        }

        public int PlayerTwoScore {
            get { return _playerTwoScore; }
            set {
                _playerTwoScore = value;
                _playerTwoText.DisplayedString = String.Format("Player Two {0}", _playerTwoScore);
            }
        }

        private RenderWindow _window;
        private List<IEntity> _entities;
        private const int BarWidth = 4;
        private const int BarHeight = 60;
        private const int BallRadius = 5;
        private const int BallSpeed = 160;
        private const int BatSpeed = 160;
        private readonly bool AI = true;
        private readonly Text _playerOneText = new Text();
        private readonly Text _playerTwoText = new Text();
        private readonly Clock _deltaClock;
        private readonly TcpTrainer _trainerClient;
        private int _playerOneScore;
        private int _playerTwoScore;

        public Game() {
            _deltaClock = new Clock();
            if (!AI) _trainerClient = new TcpTrainer(this);
        }

        public void Start() {
            bool closed = false;

            _window = new RenderWindow(new VideoMode(700, 400), "Pong");
            _window.Closed += (a, b) => closed = true;
            PlayerOneScore = 0;
            PlayerTwoScore = 0;

            IBatController playerTwoController = new PlayerController(Keyboard.Key.W, Keyboard.Key.S);

            if (AI) {
                playerTwoController = new AiController(this);
            }

            _entities = new List<IEntity> {
                new Ball(
                    game: this,
                    position: new PVector2F((float) width / 2 - 1.5f, (float) height / 2 - ((float) BallRadius / 2)),
                    radius: BallRadius,
                    speed: BallSpeed),
                new Bat(
                    game: this,
                    controller: new PlayerController(Keyboard.Key.Up, Keyboard.Key.Down),
                    position: new PVector2F(10 - BarWidth, height / 2 - BarHeight / 2),
                    size: new PVector2F(BarWidth, BarHeight),
                    speed: BatSpeed),
                new Bat(
                    game: this,
                    controller: playerTwoController,
                    position: new PVector2F(width - 10, height / 2 - BarHeight / 2),
                    size: new PVector2F(BarWidth, BarHeight),
                    speed: BatSpeed)
            };

            _deltaClock.Restart();
            _window.SetFramerateLimit(200);

            while (!closed) {
                _window.DispatchEvents();

                float delta = _deltaClock.Restart().AsSeconds();
                _entities.ForEach(x => x.Update(delta));

                if (!AI) _trainerClient.Update();

                _window.Clear();
                _entities.ForEach(x => x.Render(_window));
                _window.Display();
            }

            if (!AI) _trainerClient.End();
        }

        public List<T> GetEntities<T>() {
            return _entities.OfType<T>().ToList();
        }

        public static void Main(string[] args) {
            new Game().Start();
        }
    }
}
