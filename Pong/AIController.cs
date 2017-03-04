using System.IO;
using System.Net.Sockets;

namespace Pong {
    public class AiController : IBatController {
        private readonly Game _game;
        private readonly TcpClient client;
        private readonly StreamWriter clientWriter;

        public AiController(Game game) {
            _game = game;

            client = new TcpClient();

            client.Connect("localhost", 43437);
            var networkStream = client.GetStream();
            clientWriter = new StreamWriter(networkStream);
            clientWriter.Write("PLAY");
        }

        public void Update(Bat bat) {

        }
    }
}
