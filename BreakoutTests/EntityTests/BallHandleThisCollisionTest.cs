using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Levelloader;
using Breakout.Balls;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using DIKUArcade.Math;
using System.IO;
using System.Diagnostics.Contracts;
using Breakout.Blocks;
using System;

namespace BreakoutTests {
    [TestFixture]
    public class CollisionHandlerTest {
        private Ball ball;
        private CollisionData collisiondataBranchUp;
        private CollisionData collisiondataBranchDown;
        private CollisionData collisiondataBranchLeft;
          
        public CollisionHandlerTest() {
            collisiondataBranchUp = new CollisionData();
            collisiondataBranchUp.Collision = true;
            collisiondataBranchUp.CollisionDir = CollisionDirection.CollisionDirUp;    


            collisiondataBranchDown = new CollisionData();
            collisiondataBranchDown.Collision = true;
            collisiondataBranchDown.CollisionDir = CollisionDirection.CollisionDirDown;    


            collisiondataBranchLeft = new CollisionData();
            collisiondataBranchLeft.Collision = true;
            collisiondataBranchLeft.CollisionDir = CollisionDirection.CollisionDirLeft;
        }

        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            ball = new Ball(new DynamicShape(new Vec2F(0.50f, 0.50f), new Vec2F(0.04f, 0.04f), new Vec2F(0.002f, 0.005f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));



        }

        //comes from down to the right hits furthest half
        [Test]
        public void TestBranchOne() {
            //Ball To Test
            ball.Shape.AsDynamicShape().Direction = new Vec2F(0.001f, 0.002f);

            Entity entity = new Entity(new DynamicShape(new Vec2F(0.5f, 0.52f), new Vec2F(0.04f, 0.02f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            ball.HandleThisCollision(collisiondataBranchDown, entity);
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.Y - -0.0021582);
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.X - 0.00058483);
        }
        

        //comes from down to the right hits closest half
        [Test]
        public void TestBranchTwo() {
            ball.Shape.AsDynamicShape().Direction = new Vec2F(0.003f, 0.002f);

            Entity entity = new Entity(new DynamicShape(new Vec2F(0.50f, 0.505f), new Vec2F(0.04f, 0.02f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            ball.HandleThisCollision(collisiondataBranchDown, entity);
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.Y - -0.0036044f);
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.X - 8.803E-5f);
        }


        //comes from down to the left hits closest half
        [Test]
        public void TestBranchThree() {
            ball.Shape.AsDynamicShape().Direction = new Vec2F(-0.002f, 0.005f);

            Entity entity = new Entity(new DynamicShape(new Vec2F(0.50f, 0.505f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));
            
            ball.HandleThisCollision(collisiondataBranchDown, entity);
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.Y -0.00527133f);
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.X - - 0.0028378);
        }


        //Comes from down to the left hits furthest half
        [Test]
        public void TestBranchFour() {
            ball.Shape.AsDynamicShape().Direction = new Vec2F(-0.005f, 0.003f);

            Entity entity = new Entity(new DynamicShape(new Vec2F(0.50f, 0.505f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            ball.HandleThisCollision(collisiondataBranchDown, entity);
            
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.Y - -0.000268);
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.X - - 0.0058247);
        }

        //Comes from up to the right hits closest half
        [Test]
        public void TestBranchFive() {
            ball.Shape.AsDynamicShape().Direction = new Vec2F(0.002f, -0.005f);

            Entity entity = new Entity(new DynamicShape(new Vec2F(0.50f, 0.505f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            ball.HandleThisCollision(collisiondataBranchUp, entity);
            
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.Y - 0.00527133f);
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.X - 0.0011013f);
        }


        //comes from up to the right hits furthest half
        [Test]
        public void TestBranchSeks() {
            ball.Shape.AsDynamicShape().Direction = new Vec2F(0.004f, -0.005f);

            Entity entity = new Entity(new DynamicShape(new Vec2F(0.50f, 0.505f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            ball.HandleThisCollision(collisiondataBranchUp, entity);
            
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.Y - 0.004344149f);
            Assert.True( 1.0E-6 > ball.Shape.AsDynamicShape().Direction.X - 0.00470407f);
        }


        // comes from up to the left hits closest half
        [Test]
        public void TestBranchSeven() {
            ball.Shape.AsDynamicShape().Direction = new Vec2F(-0.002f, -0.005f);

            Entity entity = new Entity(new DynamicShape(new Vec2F(0.40f, 0.5f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            ball.HandleThisCollision(collisiondataBranchUp, entity);
            
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.Y - 0.00527133f);
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.X - -0.001101374f);
        }


        //come from up to the left hits furthest half
        [Test]
        public void TestBranchEight() {
            ball.Shape.AsDynamicShape().Direction = new Vec2F(-0.004f, -0.005f);

            Entity entity = new Entity(new DynamicShape(new Vec2F(0.40f, 0.5f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            ball.HandleThisCollision(collisiondataBranchUp, entity);
            
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.Y - 0.00434414f);
            Assert.True(1.0E-6 > ball.Shape.AsDynamicShape().Direction.X - -0.00470407f);
        }

        //Hits side
        [Test]
        public void TestBranchNine() {
            ball.Shape.AsDynamicShape().Direction = new Vec2F(0.002f, -0.005f);

            Entity entity = new Entity(new DynamicShape(new Vec2F(0.40f, 0.5f), new Vec2F(0.04f, 0.04f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            ball.HandleThisCollision(collisiondataBranchLeft, entity);
            
            Assert.True(1.0E-6f > ball.Shape.AsDynamicShape().Direction.X - -0.002f);
            Assert.True(1.0E-6f > ball.Shape.AsDynamicShape().Direction.Y - 0.005f);
        }
    }
}