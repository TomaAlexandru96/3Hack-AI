using System.IO;
using System.Net.Sockets;

namespace Pong {
    public class AiController : IBatController {
        private readonly Game _game;
        private readonly TcpClient _client;
        private readonly StreamWriter _clientWriter;

        public AiController(Game game) {
            _game = game;

            _client = new TcpClient();

            _client.Connect("localhost", 43437);
            var networkStream = _client.GetStream();
            _clientWriter = new StreamWriter(networkStream);
            _clientWriter.Write("PLAY");
        }

        public void Update(Bat bat) {
            var playerOne = _game.GetEntities<Bat>()[0];
            var playerTwo = _game.GetEntities<Bat>()[1];
            var ball = _game.GetEntities<Ball>()[0];

            _clientWriter.Write("{0} {1} {2} {3} {4} {5} {6},",
                ball.Position.X,
                ball.Position.Y,
                ball.Velocity.X,
                ball.Velocity.Y,
                ball.EffectiveSpeed,
                playerOne.Position.Y,
                playerTwo.Position.Y);
        }
    }
}
