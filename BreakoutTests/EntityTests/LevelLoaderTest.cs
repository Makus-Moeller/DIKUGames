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

namespace BreakoutTests
{
    public class LevelLoaderTests {
        private StreamReaderClass StreamReader;
        private StringTxtInterpreter StringTxtInterpreterOnEmptyArray;
        private StringTxtInterpreter StringTxtInterpreterOnPowerUpAndHardened;
        private StringTxtInterpreter StringTxtInterpreterOnPowerUpAndUnbreakable;
        private LevelLoader levelLoader;

        public LevelLoaderTests() {
            
        }

        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();

            StreamReader = new StreamReaderClass();

            StringTxtInterpreterOnEmptyArray = new StringTxtInterpreter(new StreamReaderClass());

            StringTxtInterpreterOnPowerUpAndHardened = 
                new StringTxtInterpreter(new StreamReaderClass());

            StringTxtInterpreterOnPowerUpAndUnbreakable = 
                new StringTxtInterpreter(new StreamReaderClass());

            levelLoader = new LevelLoader(Path.Combine("../../../..", "Breakout", "Assets"));
        }

        //
        [Test]
        public void StreamReaderClassTest() {
            //Vi tester at den med constraints kan generere en korrekt længde array
            Assert.AreEqual((new string[0]).Length, StreamReader.ToStringArray(
                Path.Combine("../../../..", "Breakout", "Assets", "Levels", "empty.txt"), 
                    "Map:", "Map/").Length);
            Assert.AreEqual((new string[37]).Length, StreamReader.ToStringArray(
                Path.Combine("../../../..", "Breakout", "Assets", "Levels", "level1.txt"), 
                    "Map:", "Legend/").Length);
        }

        [Test]
        //Tjekker at hvis den får en tom string kan den stadig godt bruges
        public void StringTxtInterpreterOnEmptyArrayTest() {
            StringTxtInterpreterOnEmptyArray.ReadFile(
                Path.Combine("../../../..", "Breakout", "Assets", "Levels", "empty.txt"));
            Assert.AreEqual(
                new CharDefiners[0], StringTxtInterpreterOnEmptyArray.CreateCharDefiners());
        }
        
        [Test]
        //Tjekker at den laver chardefiners baseret på arrayets fortolkning
        public void StringTxtInterpreterOnFullArrayTest() {
        //Loading
        StringTxtInterpreterOnPowerUpAndHardened.ReadFile(
            Path.Combine("../../../..", "Breakout", "Assets", "Levels", "level1.txt"));
        StringTxtInterpreterOnPowerUpAndUnbreakable.ReadFile(
            Path.Combine("../../../..", "Breakout", "Assets", "Levels", "level3.txt"));
        
        //Checking that attributes are atributed accuratly using Level 1
        Assert.False(StringTxtInterpreterOnPowerUpAndHardened.CreateCharDefiners()[0].hardened);
        Assert.False(StringTxtInterpreterOnPowerUpAndHardened.CreateCharDefiners()[1].hardened);
        Assert.True(StringTxtInterpreterOnPowerUpAndHardened.CreateCharDefiners()[2].powerUp);
        Assert.False(StringTxtInterpreterOnPowerUpAndHardened.CreateCharDefiners()[3].hardened);


        //Checking that attributes are atributed accuratly using level 3
        Assert.False(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[0].unbreakable);
        Assert.False(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[1].unbreakable);
        Assert.False(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[2].unbreakable);
        Assert.True(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[3].powerUp);
        Assert.True(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[4].unbreakable);
        Assert.False(StringTxtInterpreterOnPowerUpAndUnbreakable.CreateCharDefiners()[5].unbreakable);
        }
        
        /*
        [Test]
        public void LevelLoaderTest() {
            levelLoader.SetLevel(Path.Combine("../../../..", "Breakout", "Assets", "Levels", "level3.txt"),
                new StringTxtInterpreter(new StreamReaderClass()), new BlockCreator());
            int amountOfBLocks = 0;
            foreach (AtomBlock block in levelLoader.AllBlocks) {
                amountOfBLocks++;
            }
            Assert.AreEqual(76, amountOfBLocks);
        }
        */
    }
}