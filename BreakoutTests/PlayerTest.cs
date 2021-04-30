using NUnit.Framework;
using Breakout.Players;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System.Diagnostics.Contracts;


namespace BreakoutTests
{
    public class PlayerTests
    {
        private Player player;
        private Player TestRightLimitPlayer;

        private Player TestLeftLimitPlayer;

        [SetUp]
        public void Setup()
        {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")),
                new RegularBuffState()); 
            TestRightLimitPlayer = new Player(
                new DynamicShape(new Vec2F(1.0f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")),
                new RegularBuffState()); 
            TestLeftLimitPlayer = new Player (
                new DynamicShape(new Vec2F(0.0f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")),
                new RegularBuffState()); 
        }

        [Test]
        public void TestPosition()
        {
            player.Shape.SetPosition(new Vec2F (0.5f, 0.10f));
            Assert.True(10E-8f > player.GetPosition().X - 0.5f);
        }
        [Test]
        public void TestPlayerMoveRight()
        {
            player.SetMoveRight(true);
            player.Move();
            Assert.True(10E-8f > player.GetPosition().X - 0.465f);
        }
        [Test]
        public void TestPlayerMoveLeft()
        {
            player.SetMoveLeft(true);
            player.Move();
            Assert.True(10E-8f > player.GetPosition().X - 0.465f);
        }
        [Test]
        public void TestMoveRightLimit()
        {
            TestRightLimitPlayer.SetMoveRight(true);
            TestRightLimitPlayer.Move();
            Assert.True(10E-8f > TestRightLimitPlayer.GetPosition().X - 1.0f);
        }
        [Test]
        public void TestMoveLeftLimit()
        {
            TestLeftLimitPlayer.SetMoveLeft(true);
            TestLeftLimitPlayer.Move();
            Assert.True(10E-8f > TestLeftLimitPlayer.GetPosition().X - 0.0f);
        }
    }
}