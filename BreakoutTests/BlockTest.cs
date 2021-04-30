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
    public class BlockTests
    {
        [SetUp]
        public void Setup()
        {
            testBlock = new AtomBlock (new DynamicShape(position, 
                new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                new Image(
                    Path.Combine("..", "Breakout", "Assets", "Images", 
                        charDefiner.imagePath))
            );
            testPowerBlock = new PowerUpBlock(new DynamicShape(position, 
                new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                new Image(
                    Path.Combine("..", "Breakout", "Assets", "Images", 
                        charDefiner.imagePath))
            );
            testUnbreakableBlock = new UnbreakableBlock(
                new DynamicShape(position, 
                new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                new Image(
                    Path.Combine("..", "Breakout", "Assets", "Images",
                        charDefiner.imagePath))
            );
            testHardenedBlock = new HardenedBlock(
                new DynamicShape(position, 
                new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                new Image(
                    Path.Combine("..", "Breakout", "Assets", "Images", 
                        charDefiner.imagePath))
            );
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