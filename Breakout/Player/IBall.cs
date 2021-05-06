using DIKUArcade.Physics;
using DIKUArcade.Entities;
namespace Breakout.Players {
    
    public interface IBall {
        void ChangeDirection(CollisionData collisionData);
        void HandleCollision(Entity comparator);
    }

}