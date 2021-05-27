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
            value += 2;
        }
        
        public override void HandleThisCollision(CollisionData data, Entity objectOfCollision) {
            //Lav Event Her Til PowerUpHandler
            HitBlock(20);
            if (IsDeleted()) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{EventType = GameEventType.ControlEvent,
                    Message = "CreatePowerUp", 
                        StringArg1 = Shape.Position.X.ToString(), 
                            StringArg2 = Shape.Position.Y.ToString()});
            
            }
        } 
    }   
}