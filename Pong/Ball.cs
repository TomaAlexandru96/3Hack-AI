using SFML.Graphics;

namespace Pong {
    public class Ball : ICollideable, IEntity {
        private int _size;

        public Ball(int size) {
            _size = size;
        }

        public bool CollidesWith(ICollideable other) {
            throw new System.NotImplementedException();
        }

        public IntRect GetBoundingBox() {
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
