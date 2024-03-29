using NUnit.Framework;
using Galaga;
using DIKUArcade.EventBus;
using Galaga.GalagaStates;
using System.Collections.Generic;

namespace GalagaTests {
    [TestFixture]
    public class StateMachineTesting {
        private StateMachine stateMachine;
        
        public StateMachineTesting() {
            DIKUArcade.Window.CreateOpenGLContext();
           
        }
        [SetUp]
        public void SetUpMethod() {
            GalagaBus.GetBus();
            stateMachine = new StateMachine();
            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> { 
                GameEventType.InputEvent, 
                GameEventType.PlayerEvent, 
                GameEventType.GameStateEvent,
                GameEventType.WindowEvent,
                GameEventType.StatusEvent}); 
            //subscribing objects and eventtypes
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, stateMachine);
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            // Here you should:
            // (1) Initialize a GalagaBus with proper GameEventTypes
            // (2) Instantiate the StateMachine
            // (3) Subscribe the GalagaBus to proper GameEventTypes
            // and GameEventProcessors
            
        }

        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }
        
        [Test]
        public void TestEventGamePaused() {
            GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent,
                    this,
                    "CHANGE_STATE",
                    "GAME_PAUSED", ""));
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
        }
        
        [Test]
        public void TestEventGameRunning() {
            GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent,
                    this,
                    "CHANGE_STATE",
                    "GAME_RUNNING", ""));
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
        }
    }
}