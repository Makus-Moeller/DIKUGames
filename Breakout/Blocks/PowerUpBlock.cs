using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;

namespace Breakout.Blocks {

    
    /// <summary>
    /// PowerUp block holds an PowerUpItem. A dynamic shape that player can cath
    /// </summary>
    /// <param name="decrementValue">Amount a block is hit by</param>
    public class PowerUpBlock : AtomBlock {
        public PowerUpBlock(Shape shape, IBaseImage image) : base(shape, image) {
            PowerUpItem = new Entity(new DynamicShape(Shape.Position, new Vec2F(0.02f, 0.02f), 
                new Vec2F(0.0f, -0.01f)), 
                    new Image(Path.Combine("..", "Breakout", 
                        "Assets", "Images", "BigPowerUp.png")));
            value += 2;
        } 
    }   
}