using DIKUArcade.Math;
using Breakout.Players;

namespace Breakout.Players {
    /// <summary>
    /// Playerbuffstate with increased movementspeed. 
    /// </summary>
    public class SpeedBuffState : IBuffState {
        private const float MOVEMENT_SPEED = 0.03f;
        private Vec2F EXTENT = (new Vec2F(0.2f, 0.03f));
        public Vec2F GetExtent() {
            return EXTENT;
        }

        public float GetSpeed() {
            return MOVEMENT_SPEED;
        }
    }
}