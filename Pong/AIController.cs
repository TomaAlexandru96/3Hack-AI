using System.Net.Sockets;

namespace Pong {
    public class AiController : IBatController {
        private TcpClient client;

        public AiController() {
            client = new TcpClient("localhost", 43437);

        }

        public void Update(Bat bat) {
            throw new System.NotImplementedException();
        }
    }
}
