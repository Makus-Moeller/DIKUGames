using DIKUArcade.Entities;
using DIKUArcade.Physics;

namespace Breakout.Balls
{

    public class NullCollidable : ICollidable {
        public void HandleThisCollision(CollisionData data, Entity objectOfCollision) {}
    }
}