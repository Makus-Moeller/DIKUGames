using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
namespace Breakout.PowerUpSpace {
    
    /// <summary>
    /// A wall entity that prevents the ball to leave screen. 
    /// </summary>
    public class Wall : Entity, IGameEventProcessor {

        public bool IsActive{get; private set;}

        public Wall(Shape shape, IBaseImage image) : base(shape, image) {
            IsActive = false;
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, this);
        }

        /// <summary>
        /// Process gameevents that class is subscribed to. 
        /// </summary>
        /// <param name="gamevent">GameEvent that should be processed</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "HandlePowerUp") {
                if (gameEvent.EventType ==  GameEventType.ControlEvent) {
                    switch (PowerUpTransformer.TransformStringToPowerUp(gameEvent.StringArg1)) {
                        case PowerUps.Wall:
                            if (IsActive){
                                BreakoutBus.GetBus().ResetTimedEvent(2, TimePeriod.NewSeconds(10.0));
                            }
                            else {
                                IsActive = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else if (gameEvent.EventType ==  GameEventType.TimedEvent) {
                    switch (PowerUpTransformer.TransformStringToPowerUp(gameEvent.StringArg1)) {
                        case PowerUps.Wall:
                            IsActive = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        
        public void RenderWall() {
            if (IsActive) {
                RenderEntity();
            }
        }
    }
}