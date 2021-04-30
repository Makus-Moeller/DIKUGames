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
    public class LevelLoaderTests {
        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            levelLoader = new levelLoader();
            levelLoader.SetLevel(Path.Combine("Assets", "Levels", "level3.txt"), 
                new StringTxtInterpreter(new StreamReaderClass()), new BlockCreator());
        }


        [Test]
        public void StreamReaderClassTest() {
            
        }
        
        [Test]
        public void StringTxtInterpreterTest() {
            
        }

        [Test]
        public void LevelLoaderTest() {
            
        }
        
    }
}