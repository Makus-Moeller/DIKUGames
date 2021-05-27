using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
namespace Breakout.PowerUpSpace {
    
    public class Wall : Entity, IGameEventProcessor {

        public bool IsActive{get; private set;}

        public Wall(Shape shape, IBaseImage image) : base(shape, image) {
            IsActive = false;
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, this);
        }

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