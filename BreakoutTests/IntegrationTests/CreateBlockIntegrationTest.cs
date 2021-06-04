using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System.Diagnostics.Contracts;
using Breakout.Levelloader;
using DIKUArcade.Utilities;
using System.Collections.Generic;

namespace BreakoutTests {

    [TestFixture]    
    public class CreateBlockIntegrationTest {
        private StreamReaderClass streamReader;
        private StringTxtInterpreter txtInterpreter;
        private BlockCreator blockCreator;

        [SetUp]
        public void Setup() {
            streamReader = new StreamReaderClass();
            txtInterpreter = new StringTxtInterpreter(streamReader);
            blockCreator = new BlockCreator();
        }

        [Test]
        public void IntegrateStreamReadertxtInterpreter() {
            txtInterpreter.ReadFile(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", "FakeMapForTesting.txt"));
            CharDefiners[] charDefiners = txtInterpreter.CreateCharDefiners();

            //ChecksAllvaluesCorrectForFirstBlock
            Assert.AreEqual('u', charDefiners[0].character);
            Assert.True(charDefiners[0].unbreakable);
            Assert.AreEqual("teal-block.png", charDefiners[0].imagePath);

            //ChecksAllValuesCorrectForSecondBlock
            Assert.AreEqual('c', charDefiners[1].character);
            Assert.True(charDefiners[1].hardened);
            Assert.AreEqual("blue-block.png", charDefiners[1].imagePath);

            //CheckAllValuesCorrectForThirdBlock
            Assert.AreEqual('q', charDefiners[2].character);
            Assert.True(charDefiners[2].powerUp);
            Assert.AreEqual("green-block.png", charDefiners[2].imagePath); 
        }
        [Test]
        public void IntegrateStreamReadertxtInterpreterBlockCreator() {
            txtInterpreter.ReadFile(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", "FakeMapForTesting.txt"));
            CharDefiners[] charDefiners = txtInterpreter.CreateCharDefiners();
            EntityContainer<AtomBlock> Blocks = blockCreator.CreateBlocks(charDefiners);
            List<AtomBlock> listblock = new List<AtomBlock>();
            foreach (AtomBlock block in Blocks) {
                listblock.Add(block);
            }
            Assert.True(listblock[0] is UnbreakableBlock);
            Assert.True(listblock[1] is HardenedBlock);
            Assert.True(listblock[2] is PowerUpBlock);
        }
    }
}