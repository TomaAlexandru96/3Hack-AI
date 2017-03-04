using SFML.Graphics;
using SFML.Window;

namespace Pong {
    public class Bat : ICollideable, IEntity {
        private IBatController _controller;
        private Vector2i _position;
        private Vector2i _size;
        private Image image;
        private int velocity;

        public Bat(IBatController controller, Vector2i position, Vector2i size, Texture texture) {
            _controller = controller;
            _position = position;
            _size = size;
            Sprite bat = new Sprite(texture);
        }

        public bool CollidesWith(ICollideable other) {

            return GetBoundingBox().Intersects(other.GetBoundingBox());
        }

        public IntRect GetBoundingBox() {
            return new IntRect(_position.X, _position.Y, _size.X, _size.Y);
        }


        public void Update() {
            throw new System.NotImplementedException();
        }

        public void Render() {
            throw new System.NotImplementedException();
        }
    }
}
