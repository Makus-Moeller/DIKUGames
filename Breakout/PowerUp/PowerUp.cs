using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using DIKUArcade.Events;
using Breakout.Players;
using Breakout.Levelloader;
using DIKUArcade.Input;
using DIKUArcade.Timers;
using Breakout.Blocks;
using DIKUArcade.Physics;


namespace Breakout.PowerUpSpace {
    
    public class PowerUp : Entity, ICollidable {
        public PowerUps ThisPowerUp {get; set;}

        public PowerUp(Shape shape, IBaseImage image, PowerUps powerUpType) : base(shape, image) {
            ThisPowerUp = powerUpType;
            
        }

        public void HandleThisCollision(CollisionData data, Entity objectOfCollision) {
            if ((objectOfCollision as Player) != null) {
                DeleteEntity();
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{EventType = GameEventType.ControlEvent, 
                    Message = "HandlePowerUp",
                    StringArg1 = PowerUpTransformer.TransformPowerUpToString(ThisPowerUp)});
                
                BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{EventType = GameEventType.TimedEvent, 
                    Message = "HandlePowerUp",
                    StringArg1 = PowerUpTransformer.TransformPowerUpToString(ThisPowerUp), Id = 1 }, TimePeriod.NewSeconds(10.0));
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