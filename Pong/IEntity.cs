using SFML.Graphics;

namespace Pong {
    public interface IEntity {
        void Update();
        void Render(RenderTarget target);
    }
}
