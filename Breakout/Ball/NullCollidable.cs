using DIKUArcade.Entities;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;

namespace Breakout.Balls {

    public class NullCollidable : ICollidable {
        public void HandleThisCollision(CollisionData data, Entity objectOfCollision) {}
    }
}