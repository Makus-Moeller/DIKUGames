using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;
namespace Breakout.Players {
    public class Ball : Entity {
        public Ball(Shape shape, IBaseImage image) : base(shape, image) {
        }

        private bool HitWall() {
            return false;

        }
        private AtomBlock ifBlockHit(Entity possibleblock) {
            AtomBlock block = null;
            if ((block = (possibleblock as AtomBlock)) != null) {
                    block.HitBlock(10);
                    Console.WriteLine(block.GetHitpoints());
                    return (block);
            }
            else {
                return null;
            }
        }
        public void HandleCollision(Entity comparator) {
            var dynamicDownCast = this.Shape.AsDynamicShape();
            CollisionData collisiondata = CollisionDetection.Aabb(dynamicDownCast, comparator.Shape);
            if(collisiondata.Collision) {
                ifBlockHit(comparator);
                UpdateDirection(collisiondata);
            }
        }

        public void UpdateDirection(CollisionData collisiondata) {
            switch (collisiondata.CollisionDir) {
                case (CollisionDirection.CollisionDirUp):
                    this.Shape.AsDynamicShape().Direction.Y = -this.Shape.AsDynamicShape().Direction.Y;
                    this.Shape.AsDynamicShape().Direction.X = this.Shape.AsDynamicShape().Direction.X - 0.001f;
                    break;
                case (CollisionDirection.CollisionDirDown):
                    this.Shape.AsDynamicShape().Direction.Y = -this.Shape.AsDynamicShape().Direction.Y;
                    this.Shape.AsDynamicShape().Direction.X = this.Shape.AsDynamicShape().Direction.X - 0.001f;
                    break;
                case (CollisionDirection.CollisionDirLeft):
                    this.Shape.AsDynamicShape().Direction.X = -this.Shape.AsDynamicShape().Direction.X;
                    break;
                case (CollisionDirection.CollisionDirRight):
                    this.Shape.AsDynamicShape().Direction.X = -this.Shape.AsDynamicShape().Direction.X;
                    break;
                default:
                    break;
            }
        }
        public void MoveBall() {
            this.Shape.AsDynamicShape().Move();
        }
    }
}