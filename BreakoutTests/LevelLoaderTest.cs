using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Levelloader;
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

            StreamReader = new StreamReaderClass();

            StringTxtInterpreter = new StringTxtInterpreter();

            levelLoader = new levelLoader();
        }


        [Test]
        public void StreamReaderClassTest() {
            Assert.AreEqual(new string[0], StreamReader.ToStringArray("empty.txt"));
            Assert.AreEqual(new string[24], StreamReader.ToStringArray("level1.txt"));
        }

        [Test]
        public void StringTxtInterpreterTest() {
            
        }

        [Test]
        public void LevelLoaderTest() {
            levelLoader.SetLevel(Path.Combine("Assets", "Levels", "level3.txt"), 
                new StringTxtInterpreter(new StreamReaderClass()), new BlockCreator());
        }    
    }
}