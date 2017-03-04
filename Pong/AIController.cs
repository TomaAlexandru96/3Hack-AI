using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Markup;

namespace Pong {
    public class AiController : IBatController {
        private readonly Game _game;
        private readonly TcpClient _client;
        private readonly StreamWriter _clientWriter;
        private readonly StreamReader _clientReader;
        private readonly Thread _readThread;
        private PVector2F _velocity = new PVector2F(0, 0);

        public AiController(Game game) {
            _game = game;

            _client = new TcpClient();

            _client.Connect("localhost", 43437);
            var networkStream = _client.GetStream();
            _clientWriter = new StreamWriter(networkStream);
            _clientReader = new StreamReader(networkStream);
            _clientWriter.Write("PLAY");

            _readThread = new Thread(Start);
            _readThread.Start();
        }

        private void Start(object o) {
            string buffer = "";
            while (_client.Connected) {
                buffer += char.ConvertFromUtf32(_clientReader.Read());
                if (buffer.Count(x => x == ' ') >= 3) {
                    float[] values = new float[3];
                    int i = 0;
                    foreach (var val in buffer.Split(' ')) {
                        Console.WriteLine(val);
                        values[i] = float.Parse(val.Substring(0, 10));
                        if (++i == 3) break;
                    }

                    buffer = buffer.Split(' ').ToList().Skip(3).Aggregate((acc, s) => acc + " " + s).TrimStart();

                    if (values[0] > values[1] && values[0] > values[2]) {
                        _velocity = new PVector2F(0, 1);
                    } else if (values[1] > values[2]) {
                        _velocity = new PVector2F(0, -1);
                    } else {
                        _velocity = new PVector2F(0, 0);
                    }
                }
            }
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

            bat.Velocity = _velocity;
        }
    }
}
