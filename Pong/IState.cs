using SFML.Graphics;

namespace Pong {
    public abstract class IState {
        public abstract void Render(RenderTarget target);
        public abstract void update(float delta);
    }
}
