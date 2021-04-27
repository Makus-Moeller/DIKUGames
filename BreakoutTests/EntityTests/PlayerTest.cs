using NUnit.Framework;
using Breakout.Players;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;

namespace BreakoutTests
{
    public class PlayerTests
    {
        private Player player;

        [SetUp]
        public void Setup()
        {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png"))); 
        }

        [Test]
        public void TestPosition()
        {
            player.shape.Position.X = -0.5f;
            Assert.True(player.GetPosition().X > 0.00f);
        }
    }
}