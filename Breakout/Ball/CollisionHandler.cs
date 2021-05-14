using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;
namespace Breakout.Players {
    public class CollisionHandler {
        private Player player;
        private Ball ball;
        private EntityContainer<AtomBlock> comparator;
        public CollisionHandler(Player onlyPlayer, Ball onlyBall, EntityContainer<AtomBlock> blocks) {
            player = onlyPlayer;
            ball = onlyBall;
            InitializeCollisionHandler(blocks);
        }
        public void InitializeCollisionHandler(EntityContainer<AtomBlock> blocks) {
            comparator = blocks;
        }

        private void IfBlockHit(Entity possibleblock) {
            AtomBlock block = null;
            if ((block = (possibleblock as AtomBlock)) != null) {
                    block.HitBlock(10);
            }
        }

        private void HandleCollision(Entity comparator) {
            var dynamicDownCast = ball.Shape.AsDynamicShape();
            CollisionData collisiondata = CollisionDetection.Aabb(dynamicDownCast, comparator.Shape);
            if(collisiondata.Collision) {
                IfBlockHit(comparator);
                CalculateNewDirection(collisiondata, comparator);
            }
        }

        public void CalculateNewDirection(CollisionData collisiondata, Entity comparator) {
            switch (collisiondata.CollisionDir) {
                case (CollisionDirection.CollisionDirUp):
                case (CollisionDirection.CollisionDirDown):
                    var dynamicDownCast = ball.Shape.AsDynamicShape();
                    var factorOfChange = 1.0f + (comparator.Shape.Position.X + (comparator.Shape.Extent.X / 2.00f)) - ball.Shape.Position.X;
                    dynamicDownCast.Direction.Y = Math.Min(dynamicDownCast.Direction.Y * -factorOfChange, (float) ball.speedOfBall - 0.0005f);
                    var negOrPos = 1.0f; 
                    if (dynamicDownCast.Direction.X < 0.00f) {
                        negOrPos = -1.0f;
                    }
                    dynamicDownCast.Direction.X = negOrPos * ((float) Math.Sqrt(Math.Pow(ball.speedOfBall, 2.0) -  Math.Pow(dynamicDownCast.Direction.Y, 2.0)));
                    break;
                case (CollisionDirection.CollisionDirRight):
                case (CollisionDirection.CollisionDirLeft):
                    var dynamicDownCast2 = ball.Shape.AsDynamicShape();
                    var factorOfChange2 = 1.0f + (comparator.Shape.Position.Y + (comparator.Shape.Extent.Y / 2.00f)) - ball.Shape.Position.Y;
                    dynamicDownCast2.Direction.X = Math.Min(dynamicDownCast2.Direction.X * -factorOfChange2, (float) ball.speedOfBall - 0.0005f);
                    var negOrPos2 = 1.0f; 
                    if (dynamicDownCast2.Direction.Y < 0.00f) {
                        negOrPos2 = -1.0f;
                    }
                    dynamicDownCast2.Direction.Y = negOrPos2 * ((float) Math.Sqrt(Math.Pow(ball.speedOfBall, 2.0) -  Math.Pow(dynamicDownCast2.Direction.X, 2.0)));
                    break;
                default:
                    break;
            }
        }
        public void HandleCollisions() { 
            comparator.Iterate(block => HandleCollision(block));
            HandleCollision(player);
        }
    }
}