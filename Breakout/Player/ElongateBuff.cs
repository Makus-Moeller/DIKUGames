using DIKUArcade.Math;


namespace Breakout.Players {
    public class ElongateBuffState : IPlayerBuffState {
        private const float MOVEMENT_SPEED = 0.015f;
        private Vec2F EXTENT = (new Vec2F(0.3f, 0.03f));
        public float Getspeed() {
            return MOVEMENT_SPEED;
        }

        public Vec2F GetExtent() {
            return EXTENT;
        }
    }
}