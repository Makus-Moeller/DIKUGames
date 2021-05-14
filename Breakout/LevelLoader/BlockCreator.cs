using System.IO;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Levelloader {
    
    /// <summary>
    ///Håndterer creation af blocks
    /// </summary>
    public class BlockCreator : IBlockCreator {
        private EntityContainer<AtomBlock> blocks = new EntityContainer<AtomBlock>();  
        
        /// <summary>
        ///Creates a list of blocks based on chardefiners
        /// </summary>
        /// <param name="CharDefiners">The different type of blocks in a game</param>
        public EntityContainer<AtomBlock> CreateBlocks(CharDefiners[] charDefiners) {
            foreach (CharDefiners charDefiner in charDefiners) {
                foreach (Vec2F position in charDefiner.listOfPostions) {
                    if (charDefiner.hardened) {
                        string path = charDefiner.imagePath;
                        blocks.AddEntity(new HardenedBlock(new DynamicShape(position, 
                        new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                        new Image(Path.Combine("..", "Breakout", 
                            "Assets", "Images", path)), path));
                    }
                    else if (charDefiner.powerUp) {
                        blocks.AddEntity(new PowerUpBlock(new DynamicShape(position, 
                        new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                        new Image(Path.Combine("..", "Breakout", 
                            "Assets", "Images", charDefiner.imagePath))));
                    }
                    else if (charDefiner.unbreakable) {
                        blocks.AddEntity(new UnbreakableBlock(new DynamicShape(position, 
                        new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                        new Image(Path.Combine("..", "Breakout", 
                            "Assets", "Images", charDefiner.imagePath))));
                     }
                    else {
                        blocks.AddEntity(new AtomBlock(new DynamicShape(position, 
                            new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                            new Image(Path.Combine("..", "Breakout", 
                                "Assets", "Images", charDefiner.imagePath))));
                    }
                }
            }
            return blocks;
        } 
    }
}