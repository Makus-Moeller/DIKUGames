using NUnit.Framework;

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
            Assert.true(testBlock.GetHitpoints() = 9);
        }

        [Test]
        public void TestAddHP()
        {
            testBlock.AddHitPoint(2);
            Assert.true(testBlock.GetHitpoints() = 12);
        }

        [Test]
        public void TestHardenedBlockHP()
        {
            Assert.true(testHardenedBlock.GetHitpoints() = 14);
        }

        [Test]
        public void TestHardenedBlockIsHardened()
        {
            Assert.true(testHardenedBlock.IsHardened() = true);
        }  

        [Test]
        public void TestUnbreakableBlock()
        {
            testUnbreakableBlock.HitBlock();
            testUnbreakableBlock.HitBlock();
            testUnbreakableBlock.HitBlock();
            testUnbreakableBlock.HitBlock();
            Assert.true(testUnbreakableBlock.GetHitpoints() = 10);
        } 


    }
}