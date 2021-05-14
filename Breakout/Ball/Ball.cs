using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;

namespace Breakout.Players
{
    /// <summary>
    /// The implementation of a moveable Ball
    /// Ball is in charge of moving itself 
    /// correctly
    /// </summary>
    public class Ball : Entity {
        public readonly double speedOfBall;
        public Ball(Shape shape, IBaseImage image) : base(shape, image) {
            var dyshape = Shape.AsDynamicShape();
            speedOfBall = Math.Sqrt(Math.Pow(dyshape.Direction.X, 2.00f) + 
                Math.Pow(dyshape.Direction.Y, 2.00f));
        }

        /// <summary>
        /// Ensures that ball changes direction if it
        /// Breaches the boundris of the map
        /// </summary>
        public void HitWall() {
            if (Shape.Position.X > 0.97f) {
                Shape.AsDynamicShape().Direction.X = -Shape.AsDynamicShape().Direction.X;
            }
            else if (Shape.Position.X < 0.00f) {
                Shape.AsDynamicShape().Direction.X = -Shape.AsDynamicShape().Direction.X;
            }
            else if (Shape.Position.Y > 0.97f) {
                Shape.AsDynamicShape().Direction.Y = -Shape.AsDynamicShape().Direction.Y;
            }
            else if (Shape.Position.Y < 0.01f) {
                this.DeleteEntity();
            }
        }

        /// <summary>
        /// uses the dynamic shape of Entity
        /// to move the ball along direction vector
        /// </summary>
        public void MoveBall() {
            if (!IsDeleted()) {
                Shape.AsDynamicShape().Move();
                HitWall();
            }
        }

        public void RenderBall() {
            if (!IsDeleted()) {
                RenderEntity();
            }
        }
    }
}