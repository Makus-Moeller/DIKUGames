using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System.Diagnostics.Contracts;

namespace BreakoutTests {

    public class BlockTests {
    
        private AtomBlock testBlock;

        private PowerUpBlock testPowerBlock;

        private UnbreakableBlock testUnbreakableBlock;

        private HardenedBlock testHardenedBlock;


        [SetUp]
        public void Setup()
        {
            testBlock = new AtomBlock (new DynamicShape(new Vec2F(0.5f, 0.08f), 
                new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                new Image(
                    Path.Combine("..", "Breakout", "Assets", "Images", "blue-block.png")));

            testPowerBlock = new PowerUpBlock(new DynamicShape(new Vec2F(0.5f, 0.06f), 
                new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                new Image(
                    Path.Combine("..", "Breakout", "Assets", "Images", "blue-block.png")));
                        
            testUnbreakableBlock = new UnbreakableBlock(
                new DynamicShape(new Vec2F(0.5f, 0.07f), 
                new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                new Image(
                    Path.Combine("..", "Breakout", "Assets", "Images", "blue-block.png")));
                        
            testHardenedBlock = new HardenedBlock(
                new DynamicShape(new Vec2F(0.4f, 0.08f), 
                new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                new Image(
                    Path.Combine("..", "Breakout", "Assets", "Images", "blue-block.png")));
                        
        }

        [Test]
        public void TestHitBlock()
        {
            testBlock.HitBlock();
            Assert.True(testBlock.GetHitpoints() = 9);
        }

        [Test]
        public void TestAddHP()
        {
            testBlock.AddHitPoint(2);
            Assert.True(testBlock.GetHitpoints() = 12);
        }

        [Test]
        public void TestHardenedBlockHP()
        {
            Assert.True(testHardenedBlock.GetHitpoints() = 14);
        }

        [Test]
        public void TestHardenedBlockIsHardened()
        {
            Assert.True(testHardenedBlock.IsHardened() = true);
        }  

        [Test]
        public void TestUnbreakableBlock()
        {
            testUnbreakableBlock.HitBlock();
            testUnbreakableBlock.HitBlock();
            testUnbreakableBlock.HitBlock();
            testUnbreakableBlock.HitBlock();
            Assert.True(testUnbreakableBlock.GetHitpoints() = 10);
        } 


    }
}