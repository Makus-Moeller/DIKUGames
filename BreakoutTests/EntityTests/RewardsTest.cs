using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Levelloader;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System.Diagnostics.Contracts;
using Breakout.Blocks;
using DIKUArcade.Events; 
using System.Collections.Generic;

namespace BreakoutTests {
    public class RewardTest {
        private Rewards rewardTest;
        public GameEventBus eventbus;
        public void RewardsTests() {
            eventbus = BreakoutBus.GetBus();
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
                GameEventType.WindowEvent, GameEventType.TimedEvent, GameEventType.StatusEvent,
                    GameEventType.InputEvent, GameEventType.GameStateEvent});
        }

        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            rewardTest = new Rewards ();
        }

        [Test]
        public void TestStartAmount() {
            Assert.True(0 == rewardTest.rewards);
        }

        [Test]
        public void TestAddAmount() {
            rewardTest.ProcessEvent(new GameEvent{EventType = 
                        GameEventType.StatusEvent,
                        Message = "INCREASE_SCORE",
                        StringArg2 = "10",
                        });
            Assert.True(10 == rewardTest.rewards);
        }


    }
}