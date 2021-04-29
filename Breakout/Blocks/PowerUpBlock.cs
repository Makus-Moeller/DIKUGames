using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;

namespace Breakout.Blocks {

    //PowerUp klassen har et ekstra fiels som er et PowerUpItem. En dynamic shape
    public class PowerUpBlock : AtomBlock {
        private Entity PowerUpItem;
        public PowerUpBlock(Shape shape, IBaseImage image) : base(shape, image) {
            PowerUpItem = new Entity(new DynamicShape(Shape.Position, new Vec2F(0.02f, 0.02f), new Vec2F(0.0f, -0.01f)), 
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "BigPowerUp.png")));
        } 
    }   
}