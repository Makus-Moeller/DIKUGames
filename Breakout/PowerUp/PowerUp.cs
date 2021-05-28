using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Players;
using DIKUArcade.Timers;
using DIKUArcade.Physics;


namespace Breakout.PowerUpSpace
{
    /// <summary>
    /// Power up entity, which a player can pick up and gain new attributes.
    /// </summary>
    public class PowerUp : Entity, ICollidable {
        public PowerUps ThisPowerUp {get; set;}

        public PowerUp(Shape shape, IBaseImage image, PowerUps powerUpType) : base(shape, image) {
            ThisPowerUp = powerUpType;
            
        }

        /// <summary>
        /// Handles collision between this object and another.
        /// </summary>
        /// <param name="data">colissiondata such as position, and direction of objects</param>
        /// <param name="objectOfCollision">The object this collides with</param>

        public void HandleThisCollision(CollisionData data, Entity objectOfCollision) {
            if ((objectOfCollision as Player) != null) {
                DeleteEntity();
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{EventType = GameEventType.ControlEvent, 
                    Message = "HandlePowerUp",
                    StringArg1 = PowerUpTransformer.TransformPowerUpToString(ThisPowerUp)});
                
                if (ThisPowerUp == PowerUps.Wall) {
                    BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{EventType = GameEventType.TimedEvent, 
                    Message = "HandlePowerUp",
                    StringArg1 = PowerUpTransformer.TransformPowerUpToString(ThisPowerUp), Id = 2}, TimePeriod.NewSeconds(10.0));
                }
                else if (ThisPowerUp == PowerUps.Laser) {
                    BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{EventType = GameEventType.TimedEvent, 
                        Message = "HandlePowerUp",
                        StringArg1 = PowerUpTransformer.TransformPowerUpToString(ThisPowerUp), Id = 3 }, TimePeriod.NewSeconds(10.0));
                }
                else {
                    BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{EventType = GameEventType.TimedEvent, 
                        Message = "HandlePowerUp",
                        StringArg1 = PowerUpTransformer.TransformPowerUpToString(ThisPowerUp), Id = 1 }, TimePeriod.NewSeconds(10.0));
                }
            }    
        }

        public void Update() {
            Shape.AsDynamicShape().Move();
        }

        public void RenderPowerUp() {
            RenderEntity();
        }
    }
}