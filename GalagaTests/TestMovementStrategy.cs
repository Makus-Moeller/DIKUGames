using NUnit.Framework;
using DIKUArcade.Entities;
using Galaga.MovementStrategy;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using System;
namespace GalagaTests
{
    public class TestMovementStrategy
    {
        private Down TestDown = new Down();
        private ZigZagDown TestZigZagDown = new ZigZagDown();
        private NoMove TestNoMove = new NoMove();
        private Enemy enemyToTestMovements;
        [SetUp]
        public void Setup()
        {
            
            DIKUArcade.Window.CreateOpenGLContext();
            enemyToTestMovements = new Enemy(new DynamicShape(
                    new Vec2F (0.9f, 
                    0.9f), new Vec2F (0.1f , 0.1f)), 
                    new ImageStride(80, ImageStride.CreateStrides(
                4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"))), 
                    new ImageStride (80, ImageStride.CreateStrides(
                2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"))));
        }

        [Test]
        public void TestDownMovementStrategy() {
            //Tjekker at den rykker ned med 0.0002 når den ikke er enraged
            TestDown.MoveEnemy(enemyToTestMovements);
            Assert.True(10E-8f > enemyToTestMovements.Shape.Position.Y - (0.9f - 0.0002f));

            //Tjekker at den rykker ned med 0.0004 ekstra når den er enraged
            enemyToTestMovements.Enrage();
            enemyToTestMovements.Enrage();
            enemyToTestMovements.Enrage();
            TestDown.MoveEnemy(enemyToTestMovements);
            Assert.True(10E-8f > enemyToTestMovements.Shape.Position.Y - 
                (0.9f - 0.0002f - 0.0002f - 0.0004f));    
        }

        [Test]
        public void TestZigZagDownMovementStrategy()
        {
            //Tester Uden enraged
            TestZigZagDown.MoveEnemy(enemyToTestMovements);
            Assert.True(10E-8f > enemyToTestMovements.Shape.Position.Y - 0.9f - 0.0003f);
            Assert.True(10E-8f > enemyToTestMovements.Shape.Position.X - (0.9f + 0.05f * 
                (float) (Math.Sin((2.00f * Math.PI * (0.9f - (0.9f - 0.0003f))) / 0.045f))));
            enemyToTestMovements.Enrage();
            enemyToTestMovements.Enrage();
            enemyToTestMovements.Enrage();
            
            //Tester med Enrage
            TestZigZagDown.MoveEnemy(enemyToTestMovements);
            Assert.True(10E-8f > enemyToTestMovements.Shape.Position.Y - 
                0.9f - 0.0003f - 0.0003f - 0.0005f);
            float newStartx = 0.9f + 0.05f * (float) (Math.Sin((2.00f * Math.PI * 
                (0.9f - (0.9f - 0.0003f))) / 0.045f));
            Assert.True(10E-5f > System.MathF.Abs(enemyToTestMovements.Shape.Position.X - 
                (newStartx + 0.05f * (float) (Math.Sin((2.00f * Math.PI * 
                    ((0.9f - 0.0003f) - (0.9f - 0.0003 - 0.0003f - 0.0005f))) / 0.045f)))));
        }

        [Test]
        public void TestNoMoveMovementStrategy()
        {
            //Tjekker den ikke rykker sig med denne strategi
            TestNoMove.MoveEnemy(enemyToTestMovements);
            Assert.AreEqual(0.9f, enemyToTestMovements.Shape.Position.Y);
        }
    }
}