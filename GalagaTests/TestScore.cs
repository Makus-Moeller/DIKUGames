using NUnit.Framework;
using Galaga;
using DIKUArcade.EventBus;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
namespace GalagaTests
{
    public class TestScore
    {
        private Score TestScoreValue;
        
        [SetUp]
        public void Setup()
        {
            DIKUArcade.Window.CreateOpenGLContext();
            TestScoreValue = new Score(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f));

        }

        [Test]
        public void TestScoreIncrementMed1()
        {
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
            GalagaBus.GetBus().ProcessEvents();
            Assert.AreEqual(1, TestScoreValue.score);
        }

        [Test]
        public void TestScoreIncrementMedMinus()
        {
            //Tilføjer point så vi kan trække dem fra igen
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "1"));
            
            //Fjerne 10 point for at tjekke om værdien også virker når den er 0
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "-10"));
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.AreEqual(0, TestScoreValue.score);
            
        }

        [Test]
        public void TestScoreIncrementMed0()
        {
            GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.StatusEvent, this, "INCREASE_SCORE", "MoveRight", "0"));
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.AreEqual(0, TestScoreValue.score);
        }
    }
}