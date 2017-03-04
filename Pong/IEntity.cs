using SFML.Graphics;

namespace Pong {
    public interface IEntity {
        void Update(float delta);
        void Render(RenderTarget target);
    }
}
