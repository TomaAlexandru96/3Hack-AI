namespace Pong {
    public class Bat : ICollideable, IEntity {
        private IBatController _controller;
        private Vector2D _position;
        private Vector2D _size;

        public Bat(IBatController controller, Vector2D position, Vector2D size) {
            _controller = controller;
            _position = position;
            _size = size;
        }

        public bool CollidesWith(ICollideable other) {
            throw new System.NotImplementedException();
        }

        public void Update() {
            throw new System.NotImplementedException();
        }

        public void Render() {
            throw new System.NotImplementedException();
        }
    }
}
