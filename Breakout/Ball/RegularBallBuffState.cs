using DIKUArcade.Math;
using System;
using Breakout.Players;

namespace Breakout.Balls {
    /// <summary>
    /// Regular buffstate of ball.
    /// </summary>
    public class RegularBallBuffState : IBuffState {
        private float MOVEMENT_SPEED = (float) (Math.Sqrt(Math.Pow(0.01, 2.00) + Math.Pow(0.02, 2.00)));
        private Vec2F EXTENT = new Vec2F(0.04f, 0.04f);
        public float GetSpeed() {
            return MOVEMENT_SPEED;
        }

        public Vec2F GetExtent() {
            return EXTENT;
        }
    }
}