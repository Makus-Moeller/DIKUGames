using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Breakout;
using Breakout.Balls;


namespace Breakout.Players {

    ///<summary>
    /// Player shot class collidable with blocks.
    ///</summary> 
    public class PlayerShot : Entity, ICollidable {
        private static Vec2F extent = new Vec2F(0.008f, 0.021f);
        private static Vec2F direction = new Vec2F(0.0f, 0.1f);
        public PlayerShot(Vec2F position, IBaseImage image) : base(
                new DynamicShape(position, extent, direction), image) {            
        }

        public void HandleThisCollision(CollisionData data, Entity objectOfCollision)
        {
            this.DeleteEntity();
        }
    }
}