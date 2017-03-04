using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Vector2f = SFML.Window.Vector2f;

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
                _playerOneText.DisplayedString = String.Format("Player One : {0}", _playerOneScore);

            }
        }

        public int PlayerTwoScore {
            get { return _playerTwoScore; }
            set {
                _playerTwoScore = value;
                _playerTwoText.DisplayedString = String.Format("Player Two : {0}", _playerTwoScore);
            }
        }

        private RenderWindow _window;
        private List<IEntity> _entities;
        private const int BarWidth = 4;
        private const int BarHeight = 60;
        private const int BallRadius = 5;
        private const int BallSpeed = 160;
        private const int BatSpeed = 160;
        private readonly Text _playerOneText = new Text();
        private readonly Text _playerTwoText = new Text();
        private readonly Clock _deltaClock;
        private readonly TcpTrainer _trainerClient;
        private int _playerOneScore;
        private int _playerTwoScore;

        public IState state { get; set; }

        public Game() {
            _deltaClock = new Clock();
            _trainerClient = new TcpTrainer(this);
        }

        public void Start() {
            bool closed = false;

            _window = new RenderWindow(new VideoMode(700, 400), "Pong");
            _window.Closed += (a, b) => closed = true;
            state = new MenuState(this);

            PlayerOneScore = 0;
            PlayerTwoScore = 0;
            Font font = new Font("./arial.ttf");
            _playerOneText.Font = font;
            _playerOneText.Color = new Color(Color.White);
            _playerOneText.Position = new Vector2f(120,70);
            _playerTwoText.Font = font;
            _playerTwoText.Color = new Color(Color.White);
            _playerTwoText.Position = new Vector2f(400,70);

            _playerTwoText.CharacterSize = 30;

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
            };

            _deltaClock.Restart();
            _window.SetFramerateLimit(200);

            while (!closed ) {
                _window.DispatchEvents();

                float delta = _deltaClock.Restart().AsSeconds();
                //_entities.ForEach(x => x.Update(delta));
                state.update(delta);


                _trainerClient.Update();

                _window.Clear();
                state.Render(_window);
                //_entities.ForEach(x => x.Render(_window));
                if (state is PlayState) {
                    _window.Draw(_playerOneText);
                    _window.Draw(_playerTwoText);
                }
                _window.Display();
            }
        }

        public List<T> GetEntities<T>() {
            return _entities.OfType<T>().ToList();
        }

        public void ChangeState() {
            state = new PlayState(_entities);
        }

        public static void Main(string[] args) {
            new Game().Start();
        }

        public void AddAiPlayer() {
            Bat player = new Bat(
                game: this,
                controller: new AiController(this),
                position: new PVector2F(width - 10, height / 2 - BarHeight / 2),
                size: new PVector2F(BarWidth, BarHeight),
                speed: BatSpeed);
            _entities.Add(player);
        }

        public void AddNomralPlayer() {
            Bat player = new Bat(
                            game: this,
                            controller: new PlayerController(Keyboard.Key.W, Keyboard.Key.S),
                            position: new PVector2F(width - 10, height / 2 - BarHeight / 2),
                            size: new PVector2F(BarWidth, BarHeight),
                            speed: BatSpeed);
            _entities.Add(player);
        }
    }

}
