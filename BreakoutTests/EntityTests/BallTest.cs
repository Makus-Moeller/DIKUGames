using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Levelloader;
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
    public class BallTest {
        private Ball ball;
        
        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            ball = new Ball(new DynamicShape(new Vec2F(0.50f, 0.50f), new Vec2F(0.04f, 0.04f), new Vec2F(0.002f, 0.005f)),
                    new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));
        
        }

        [Test]
        public void MoveBallTest() {
            ball.MoveBall();
            Assert.AreEqual(0.502f, ball.Shape.Position.X);
            Assert.AreEqual(0.505f, ball.Shape.Position.Y);
            ball.DeleteEntity();
            ball.MoveBall();
            Assert.AreEqual(0.502f, ball.Shape.Position.X);
            Assert.AreEqual(0.505f, ball.Shape.Position.Y);
        }

        [Test]
        public void HitWallRightAndLeftBoundryTest() {
            ball.Shape.Position.X = 0.98f;
            ball.HitWall();
            Assert.True(1.0E-7 > ball.Shape.AsDynamicShape().Direction.X - -0.002f);
            ball.Shape.Position.X = -0.01f;
            ball.HitWall();
            Assert.True(1.0E-7 > ball.Shape.AsDynamicShape().Direction.X - 0.002f);
        } 

        [Test]
        public void HitWallUpperBoundryTest() {
            ball.Shape.Position.Y = 0.98f;
            ball.HitWall();
            Assert.True(1.0E-7 > ball.Shape.AsDynamicShape().Direction.Y - -0.005f);
        } 
        [Test]
        public void HitWallLowerBoundryTest() {
            ball.Shape.Position.Y = 0.0f;
            ball.HitWall();
            Assert.True(ball.IsDeleted());
        } 
    }
}