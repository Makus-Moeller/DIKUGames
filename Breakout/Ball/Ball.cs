using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;
namespace Breakout.Players {
    public class Ball : Entity {
        private readonly double speedOfBall;
        public Ball(Shape shape, IBaseImage image) : base(shape, image) {
            var dyshape = Shape.AsDynamicShape();
            speedOfBall = Math.Sqrt(Math.Pow(dyshape.Direction.X, 2.00f) + Math.Pow(dyshape.Direction.Y, 2.00f));
        }

        private void HitWall() {
            if (Shape.Position.X > 0.97f) {
                CollisionData CollisionData = new CollisionData();
                CollisionData.CollisionDir = CollisionDirection.CollisionDirRight;
                UpdateDirection(CollisionData, null);
            }
            else if (Shape.Position.X < 0.00f) {
                CollisionData CollisionData = new CollisionData();
                CollisionData.CollisionDir = CollisionDirection.CollisionDirLeft;
                UpdateDirection(CollisionData, null);
            }
            else if (Shape.Position.Y > 0.97f) {
                CollisionData CollisionData = new CollisionData();
                CollisionData.CollisionDir = CollisionDirection.CollisionDirUp;
                UpdateDirection(CollisionData, null);
            }
            else if (Shape.Position.Y < 0.01f) {
                this.DeleteEntity();
            }
        }

        private AtomBlock ifBlockHit(Entity possibleblock) {
            AtomBlock block = null;
            if ((block = (possibleblock as AtomBlock)) != null) {
                    block.HitBlock(10);
                    return block;
            }
            else {
                return null;
            }
        }

        private void HandleCollision(Entity comparator) {
            var dynamicDownCast = this.Shape.AsDynamicShape();
            CollisionData collisiondata = CollisionDetection.Aabb(dynamicDownCast, comparator.Shape);
            if(collisiondata.Collision) {
                ifBlockHit(comparator);
                UpdateDirection(collisiondata, comparator);
            }
        }

        private void UpdateDirection(CollisionData collisiondata, Entity comparator) {
            if (comparator != null) {
                switch (collisiondata.CollisionDir) {
                    case (CollisionDirection.CollisionDirUp):
                    case (CollisionDirection.CollisionDirDown):
                        var dynamicDownCast = Shape.AsDynamicShape();
                        var factorOfChange = 1.0f + (comparator.Shape.Position.X + (comparator.Shape.Extent.X / 2.00f)) - Shape.Position.X;
                        dynamicDownCast.Direction.Y = Math.Min(dynamicDownCast.Direction.Y * -factorOfChange, (float) speedOfBall - 0.0005f);
                        var negOrPos = 1.0f; 
                        if (dynamicDownCast.Direction.X < 0.00f) {
                            negOrPos = -1.0f;
                        }
                        dynamicDownCast.Direction.X = negOrPos * ((float) Math.Sqrt(Math.Pow(speedOfBall, 2.0) -  Math.Pow(dynamicDownCast.Direction.Y, 2.0)));
                        break;
                   /*
                    
                        var dynamicDownCast1 = Shape.AsDynamicShape();
                        var factorOfChange1 = 1.0f + (comparator.Shape.Position.X + (comparator.Shape.Extent.X / 2.00f)) - Shape.Position.X;
                        dynamicDownCast1.Direction.Y = Math.Min(dynamicDownCast1.Direction.Y * -factorOfChange1, (float) speedOfBall-0.0005f);
                        var negOrPos1 = 1.0f; 
                        if (dynamicDownCast1.Direction.X < 0.00f) {
                            negOrPos1= -1.0f;
                        }
                        dynamicDownCast1.Direction.X = negOrPos1 * ((float) Math.Sqrt(Math.Pow(speedOfBall, 2.0) -  Math.Pow(dynamicDownCast1.Direction.Y, 2.0)));
                        break;
                    */
                    case (CollisionDirection.CollisionDirRight):
                    case (CollisionDirection.CollisionDirLeft):
                        var dynamicDownCast2 = Shape.AsDynamicShape();
                        var factorOfChange2 = 1.0f + (comparator.Shape.Position.X + (comparator.Shape.Extent.Y / 2.00f)) - Shape.Position.Y;
                        dynamicDownCast2.Direction.X = Math.Min(dynamicDownCast2.Direction.X * -factorOfChange2, (float) speedOfBall - 0.0005f);
                        var negOrPos2 = 1.0f; 
                        if (dynamicDownCast2.Direction.Y < 0.00f) {
                            negOrPos2 = -1.0f;
                        }
                        dynamicDownCast2.Direction.Y = negOrPos2 * ((float) Math.Sqrt(Math.Pow(speedOfBall, 2.0) -  Math.Pow(dynamicDownCast2.Direction.X, 2.0)));
                        break;
                    default:
                        break;
                }
            }
            else {
                switch (collisiondata.CollisionDir) {
                    case (CollisionDirection.CollisionDirUp):
                    case (CollisionDirection.CollisionDirDown):
                        Shape.AsDynamicShape().Direction.Y = -Shape.AsDynamicShape().Direction.Y;
                        break;
                    case (CollisionDirection.CollisionDirLeft):
                    case (CollisionDirection.CollisionDirRight):
                        Shape.AsDynamicShape().Direction.X = -Shape.AsDynamicShape().Direction.X;
                        break;
                    default:
                        break;
                }
            }
        }

        private void MoveBall() {
            if (!IsDeleted()) {
                Shape.AsDynamicShape().Move();
                HitWall();
            }
        }

        public void UpdateBall(EntityContainer<AtomBlock> comparator, Player player) { 
            MoveBall();
            comparator.Iterate(block => HandleCollision(block));
            HandleCollision(player);  
               
        }

        public void RenderBall() {
            if (!IsDeleted()) {
                RenderEntity();
            }
        }
    }
}