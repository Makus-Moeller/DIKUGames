using System.IO;
using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Levelloader {

    //Laver en liste af blocks
    //Fordi vi laver et interface er fordi det kan være man vil lave en 
    //generator der vil lave anden størelse eller hente billeder 
    //fran en anden stig
    public class BlockCreator : IBlockCreator {
        private List<AtomBlock> blocks = new List<AtomBlock>();  
        public List<AtomBlock> CreateBlocks(CharDefiners[] charDefiners) {
            foreach (CharDefiners charDefiner in charDefiners) {
                foreach (Vec2F position in charDefiner.listOfPostions) {
                    if (charDefiner.hardened) {
                        blocks.Add(new HardenedBlock(new DynamicShape(position, 
                        new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                        new Image(Path.Combine("..", "Breakout", 
                            "Assets", "Images", charDefiner.imagePath))));
                    }
                    else if (charDefiner.powerUp) {
                        blocks.Add(new PowerUpBlock(new DynamicShape(position, 
                        new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                        new Image(Path.Combine("..", "Breakout", 
                            "Assets", "Images", charDefiner.imagePath))));
                    }
                    else if (charDefiner.unbreakable) {
                        blocks.Add(new UnbreakableBlock(new DynamicShape(position, 
                        new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                        new Image(Path.Combine("..", "Breakout", 
                            "Assets", "Images", charDefiner.imagePath))));
                     }
                    else {
                        blocks.Add(new AtomBlock(new DynamicShape(position, 
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