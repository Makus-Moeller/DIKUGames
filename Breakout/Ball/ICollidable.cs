using DIKUArcade.Entities;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;

namespace Breakout.Balls {
    public interface ICollidable {
        void HandleThisCollision(CollisionData data, Entity objectOfCollision);
    }
}