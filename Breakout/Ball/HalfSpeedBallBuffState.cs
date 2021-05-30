using DIKUArcade.Math;
using Breakout.Players;


namespace Breakout.Balls {
    
    /// <summary>
    /// Ball buffstate with half the movementspeed of the regular one.
    /// </summary>
    public class HalfSpeedBallBuffState : IBuffState {
        private const float MOVEMENT_SPEED = 0.015f;
        private Vec2F EXTENT = (new Vec2F(0.2f, 0.03f));
        public float GetSpeed() {
            return MOVEMENT_SPEED;
        }

        public Vec2F GetExtent() {
            return EXTENT;
        }
    }
}