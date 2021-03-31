using NUnit.Framework;
using Galaga;
using DIKUArcade.EventBus;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;




namespace GalagaTests
{
    public class TestPlayer
    {
        
        private Player PlayerForTest;
        private Player PlayerForTestLimitRight;
        private Player PlayerForTestLimitLeft;
        [SetUp]
        public void Setup()
        {
            DIKUArcade.Window.CreateOpenGLContext();
            PlayerForTest = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("..", "Galaga", "Assets", "Images", "Player.png"))); 

            PlayerForTestLimitRight = new Player(
                new DynamicShape(new Vec2F(0.9f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("..", "Galaga", "Assets", "Images", "Player.png"))); 

             PlayerForTestLimitLeft = new Player(
                new DynamicShape(new Vec2F(0.0f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("..", "Galaga", "Assets", "Images", "Player.png"))); 
        }
        
        //Tjekker Move() den rykker korrekt til højre
        [Test]
        public void TestPlayerMoveRight()
        {
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_RIGHT", "MoveRight", "1"));
            GalagaBus.GetBus().ProcessEvents();
            PlayerForTest.Move();
            Assert.True(10E-8f > PlayerForTest.getPosition().X - (0.45f + 0.01f));
        }
        
        //Tjekker om Move() rykker korrekt til venstre
        [Test]
        public void TestPlayerMoveLeft()
        {
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_LEFT", "MoveLeft", "1"));
            GalagaBus.GetBus().ProcessEvents();
            PlayerForTest.Move();
            Assert.True(10E-8f > PlayerForTest.getPosition().X - (0.45f - 0.01f));
        }


        //Tester at Move() den ikke kan rykke udenfor skærmen
        [Test]
        public void TestPlayerRightSideLimit()
        {
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_LEFT", "MoveLeft", "1"));
            GalagaBus.GetBus().ProcessEvents();
            PlayerForTestLimitRight.Move();
            Assert.True(10E-8f > PlayerForTestLimitRight.getPosition().X - 0.9f);
        }

        //Tester at Move() den ikke kan rykke udenfor skærmen
        [Test]
        public void TestPlayerLeftSideLimit()
        {
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_LEFT", "MoveLeft", "1"));
            GalagaBus.GetBus().ProcessEvents();
            PlayerForTestLimitLeft.Move();
            Assert.True(10E-8f > PlayerForTestLimitLeft.getPosition().X - 0.0f);
        }
    }
}