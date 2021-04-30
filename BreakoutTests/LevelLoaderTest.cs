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
        private StreamReaderClass StreamReader;
        private StringTxtInterpreter StringTxtInterpreterOnEmptyArray;
        private StringTxtInterpreter StringTxtInterpreterOnPowerUpAndHardened;
        private StringTxtInterpreter StringTxtInterpreterOnPowerUpAndUnbreakable;
        private LevelLoader levelLoader;

        public LevelLoaderTests() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
        }

        [SetUp]
        public void Setup() {

            StreamReader = new StreamReaderClass();

            StringTxtInterpreterOnEmptyArray = new StringTxtInterpreter(new StreamReaderClass());

            StringTxtInterpreterOnPowerUpAndHardened = new StringTxtInterpreter(new StreamReaderClass());

            StringTxtInterpreterOnPowerUpAndUnbreakable = new StringTxtInterpreter(new StreamReaderClass());

            levelLoader = new LevelLoader();
        }


        [Test]
        public void StreamReaderClassTest() {
            Assert.AreEqual((new string[0]).Length, StreamReader.ToStringArray("empty.txt", "Map:", "Map/").Length);
            Assert.AreEqual((new string[22]).Length, StreamReader.ToStringArray("level1.txt", "Map:", "Legend/").Length);
        }

        [Test]
        public void StringTxtInterpreterOnEmptyArrayTest() {
            StringTxtInterpreterOnEmptyArray.ReadFile(new string[0]);
            Assert.AreEqual(new CharDefiners[0], StringTxtInterpreterOnEmptyArray.CreateCharDefiners());
        }
        
        [Test]
        public void StringTxtInterpreterOnFullArrayTest() {
        //Loading
        StringTxtInterpreterOnPowerUpAndHardened.ReadFile("level1");
        StringTxtInterpreterOnPowerUpAndUnbreakable.ReadFile("level3");
        
        //Checking that attributes are atributed accuratly using Level 1
        Assert.True(StringTxtInterpreterOnPowerUpAndHardened.CreateCharDefiners()[0].hardened);
        Assert.False(StringTxtInterpreterOnPowerUpAndHardened.CreateCharDefiners()[1].hardened);
        Assert.True(StringTxtInterpreterOnPowerUpAndHardened.CreateCharDefiners()[2].powerup);
        Assert.False(StringTxtInterpreterOnPowerUpAndHardened.CreateCharDefiners()[3].hardened);


        //Checking that attributes are atributed accuratly using level 3
        Assert.False(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[0].unbreakable);
        Assert.False(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[1].unbreakable);
        Assert.False(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[2].unbreakable);
        Assert.True(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[3].powerup);
        Assert.True(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[4].unbreakable);
        Assert.False(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[5].unbreakable);
        }

        [Test]
        public void LevelLoaderTest() {
            levelLoader.SetLevel(Path.Combine("Assets", "Levels", "level3.txt"), 
                new StringTxtInterpreter(new StreamReaderClass()), new BlockCreator());
            Assert.False(false);
        }
        
    }
}