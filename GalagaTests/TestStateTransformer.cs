using NUnit.Framework;
using GalagaStates;

namespace GalagaTests
{
    public class TestStateTransformer
    {
        [SetUp]
        public void Setup()
        {
            DIKUArcade.Window.CreateOpenGLContext();
        }

        [Test]
        public void TestRunning()
        {
            Assert.AreEqual("GAME_RUNNING" , StateTransformer.TransformStateToString(GameStateType.GameRunning));
            Assert.AreEqual(GameStateType.GameRunning, StateTransformer.TransformStringToState("GAME_RUNNING"));
        }
        [Test]
        public void TestPaused()
        {
            Assert.AreEqual("GAME_PAUSED", StateTransformer.TransformStateToString(GameStateType.GamePaused));
            Assert.AreEqual(GameStateType.GamePaused, StateTransformer.TransformStringToState("GAME_PAUSED"));
        }
        [Test]
        public void TestMainMenu()
        {
            Assert.AreEqual("MAINMENU", StateTransformer.TransformStateToString(GameStateType.MainMenu));
            Assert.AreEqual(GameStateType.MainMenu, StateTransformer.TransformStringToState("MAINMENU"));
        }
        [Test]
        public void TestInvalidInput()
        {
            Assert.Throws<System.ArgumentException>(() => StateTransformer.TransformStringToState("Ikke Et State"));
        }
    }
}