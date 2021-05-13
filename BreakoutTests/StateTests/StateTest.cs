using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System.Diagnostics.Contracts;
using Breakout.BreakoutStates;

namespace BreakoutTests {

    public class StateTests {
    
        private StateMachine stateMachine;




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

        /*
        [Test]
        public void TestAddHP()
        {
            testBlock.AddHitpoint(2);
            Assert.True(testBlock.GetHitpoints() == 12);
        }
        */
    }
}