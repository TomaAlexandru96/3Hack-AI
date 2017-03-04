using System.Collections.Generic;
using SFML.Graphics;

namespace Pong {
    public class PlayState : IState {
        private List<IEntity> _entities;

        public PlayState(List<IEntity> entities) {
            _entities = entities;
        }

        public override void Render(RenderTarget target) {
            _entities.ForEach(x => x.Render(target));
        }

        public override void update(float delta) {
            _entities.ForEach(x => x.Update(delta));
        }
    }
}
