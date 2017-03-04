﻿using SFML.Graphics;

namespace Pong {
    public interface ICollideable {
        bool CollidesWith(ICollideable other);
        IntRect GetBoundingBox();
    }
}