using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using Breakout.Players;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    
    /// <summary>
    /// PowerUp block holds an PowerUpItem. A dynamic shape that player can cath
    /// </summary>
    public class PowerUpBlock : AtomBlock {
        public PowerUpBlock(Shape shape, IBaseImage image) : base(shape, image) {
            PowerUpItem = new Entity(new DynamicShape(Shape.Position, new Vec2F(0.02f, 0.02f), 
                new Vec2F(0.0f, -0.01f)), 
                    new Image(Path.Combine("..", "Breakout", 
                        "Assets", "Images", "BigPowerUp.png")));
            value += 2;
        }
        
        public override void HandleThisCollision(CollisionData data, Entity objectOfCollision) {
            HitBlock(20);
        } 
    }   
}