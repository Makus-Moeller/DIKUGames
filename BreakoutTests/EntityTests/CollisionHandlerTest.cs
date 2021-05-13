using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Levelloader;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using System.IO;
using System.Diagnostics.Contracts;
using Breakout.Blocks;
using System;

namespace BreakoutTests {
    [TestFixture]
    public class CollisionHandlerTest {
        private CollisionHandler collisionHandler;
        private EntityContainer<AtomBlock> blocks;
        private Player player;
        private Ball ball;
        private CollisionData collisiondataBranch1;
        private CollisionData collisiondataBranch2;
        private CollisionData collisiondataBranch3;
        private CollisionData collisiondataBranch4;
        private Entity entityForBranchOneAndTwo;
        private Entity entityForBranchThreeAndFour;    
        public CollisionHandlerTest() {
            collisiondataBranch1 = new CollisionData();
            collisiondataBranch1.Collision = true;
            collisiondataBranch1.CollisionDir = CollisionDirection.CollisionDirUp;    


            collisiondataBranch2 = new CollisionData();
            collisiondataBranch2.Collision = true;
            collisiondataBranch2.CollisionDir = CollisionDirection.CollisionDirDown;    


            collisiondataBranch3 = new CollisionData();
            collisiondataBranch3.Collision = true;
            collisiondataBranch3.CollisionDir = CollisionDirection.CollisionDirLeft;


            collisiondataBranch4 = new CollisionData();
            collisiondataBranch4.Collision = true;
            collisiondataBranch4.CollisionDir = CollisionDirection.CollisionDirRight;
        }

        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            ball = new Ball(new DynamicShape(new Vec2F(0.50f, 0.50f), new Vec2F(0.04f, 0.04f), new Vec2F(0.002f, 0.005f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));


            entityForBranchOneAndTwo = new Entity(new DynamicShape(new Vec2F(0.40f, 0.5f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));


            entityForBranchThreeAndFour = new Entity(new DynamicShape(new Vec2F(0.50f, 0.48f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));


            collisionHandler = new CollisionHandler(player, ball, blocks);

        }

        [Test]
        public void TestBranchOne() {
            collisionHandler.CalculateNewDirection(collisiondataBranch1, entityForBranchOneAndTwo);
            Assert.True(1.0E-6f >  ball.Shape.AsDynamicShape().Direction.Y - -0.0046f);
            Assert.True(1.0E-6f > ball.Shape.AsDynamicShape().Direction.X - 0.0028f );
        }

        [Test]
        public void TestBranchTwo() {
            ball.Shape.AsDynamicShape().Direction = new Vec2F(-0.002f, 0.005f);
            collisionHandler.CalculateNewDirection(collisiondataBranch2, entityForBranchOneAndTwo);
            Assert.True(1.0E-6f > ball.Shape.AsDynamicShape().Direction.Y - -0.0046f);
            Assert.True(1.0E-6f > ball.Shape.AsDynamicShape().Direction.X - -0.0028f);
        }

        [Test]
        public void TestBranchThree() {
            collisionHandler.CalculateNewDirection(collisiondataBranch3, entityForBranchThreeAndFour);
            Assert.True(1.0E-6f > ball.Shape.AsDynamicShape().Direction.X - -0.002f);
            Assert.True(1.0E-6f > ball.Shape.AsDynamicShape().Direction.Y - 0.005f);
        }

        [Test]
        public void TestBranchFour() {
            collisionHandler.CalculateNewDirection(collisiondataBranch4, entityForBranchThreeAndFour);
            ball.Shape.AsDynamicShape().Direction = new Vec2F(-0.002f, 0.005f);
            Assert.True(1.0E-6f > ball.Shape.AsDynamicShape().Direction.X - -0.002f);
            Assert.True(1.0E-6f > ball.Shape.AsDynamicShape().Direction.Y - 0.005f);
            
        }
    }
}