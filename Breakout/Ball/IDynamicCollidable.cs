using DIKUArcade.Entities;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;
namespace Breakout.Players {
    public interface ICollidable {
        void HandleThisCollision(CollisionData data, Entity objectOfCollision);
    }
}