using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;
using System.IO;
using System.Diagnostics.Contracts;
using Breakout.BreakoutStates;
using System.Collections.Generic;

namespace BreakoutTests {

    public class StateTests {
        public StateMachine stateMachine;
        public GameEventBus eventbus;
        public StateTests() {
            eventbus = BreakoutBus.GetBus();
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
                GameEventType.WindowEvent, GameEventType.TimedEvent, GameEventType.StatusEvent,
                    GameEventType.InputEvent, GameEventType.GameStateEvent});
        }
        

        [SetUp]
        public void Setup()
        {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
                        
        }

        [Test]
        public void TestInitialState()
        {
            Assert.True(stateMachine.ActiveState == MainMenu.GetInstance());
            
        }

        
        [Test]
        public void TestSwitchState()
        {
            //Der kommer en nullreference exception
            stateMachine.SwitchState(GameStateType.GameRunning, "MAINMENU");
            Assert.True(stateMachine.ActiveState == GameRunning.GetInstance());
        }

        [Test]
        public void TestProcessEvent() {
            Assert.True(MainMenu.GetInstance().activeMenuButton == 0);
            stateMachine.ProcessEvent(new GameEvent{EventType = GameEventType.InputEvent, 
                                Message = "KEY_DOWN"});
            Assert.True(MainMenu.GetInstance().activeMenuButton == 1);
            stateMachine.ProcessEvent(new GameEvent{EventType = GameEventType.InputEvent, 
                                Message = "KEY_UP"});
            Assert.True(MainMenu.GetInstance().activeMenuButton == 0);
        }

        //Test TransformStringToState og StateToString
        
    }
}