using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;
namespace Breakout.Players {
    public class Ball : Entity {
        private Random MovementRandomizer;
        public Ball(Shape shape, IBaseImage image) : base(shape, image) {
            MovementRandomizer = new Random();
        }

        private void HitWall() {
            if (Shape.Position.X > 0.97f) {
                CollisionData CollisionData = new CollisionData();
                CollisionData.CollisionDir = CollisionDirection.CollisionDirRight;
                UpdateDirection(CollisionData);
            }
            else if (Shape.Position.X < 0.00f) {
                CollisionData CollisionData = new CollisionData();
                CollisionData.CollisionDir = CollisionDirection.CollisionDirLeft;
                UpdateDirection(CollisionData);
            }
            else if (Shape.Position.Y > 0.97f) {
                CollisionData CollisionData = new CollisionData();
                CollisionData.CollisionDir = CollisionDirection.CollisionDirUp;
                UpdateDirection(CollisionData);
            }
            else if (Shape.Position.Y < 0.01f) {
                this.DeleteEntity();
            }
        }
        private float createRandomFloat() {
            int randomInt = MovementRandomizer.Next(-10, 10);
            float randomfloat = ((float) randomInt) / 10000.0f;
            return randomfloat;
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
                UpdateDirection(collisiondata);
            }
        }

        private void UpdateDirection(CollisionData collisiondata) {
            switch (collisiondata.CollisionDir) {
                case (CollisionDirection.CollisionDirUp):
                case (CollisionDirection.CollisionDirDown):
                    this.Shape.AsDynamicShape().Direction.Y = -Shape.AsDynamicShape().Direction.Y + createRandomFloat();
                    break;
                case (CollisionDirection.CollisionDirLeft):
                case (CollisionDirection.CollisionDirRight):
                    this.Shape.AsDynamicShape().Direction.X = -Shape.AsDynamicShape().Direction.X;
                    break;
                default:
                    break;
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