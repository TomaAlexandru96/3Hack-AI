using System;
using System.IO;
using System.Net.Sockets;

namespace Pong {
    public class TcpTrainer {
        private readonly Game _game;
        private readonly TcpClient _client;
        private readonly StreamWriter _clientWriter;

        public TcpTrainer(Game game) {
            _game = game;

            _client = new TcpClient();

            try {
                _client.Connect("localhost", 43437);
                var networkStream = _client.GetStream();
                _clientWriter = new StreamWriter(networkStream);
                _clientWriter.Write("TRAIN");
            } catch (Exception) {
                Console.WriteLine("[ERROR] Failed to connect to TCP AI training service");
            }
        }

        public void Update() {
            if (_clientWriter == null) return;

            var playerOne = _game.GetEntities<Bat>()[0];
            var playerTwo = _game.GetEntities<Bat>()[1];
            var ball = _game.GetEntities<Ball>()[0];

            _clientWriter.Write("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                ball.Position.X,
                ball.Position.Y,
                ball.Velocity.X,
                ball.Velocity.Y,
                ball.EffectiveSpeed,
                playerOne.Position.Y,
                playerTwo.Position.Y,
                playerOne.Velocity.Y < -0.01 ? 1 : 0,
                playerOne.Velocity.Y > 0.01 ? 1 : 0,
                Math.Abs(playerOne.Velocity.Y) < 0.01 ? 1 : 0);
        }
    }
}
