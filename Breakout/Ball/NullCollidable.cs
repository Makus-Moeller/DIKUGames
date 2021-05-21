using DIKUArcade.Entities;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;
namespace Breakout.Players {

    public class NullCollidable : ICollidable {
        public void HandleThisCollision(CollisionData data, Entity objectOfCollision) {}
    }
}