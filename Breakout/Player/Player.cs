using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using Breakout.PowerUpSpace;

namespace Breakout.Players {
    public class Player : Entity, IGameEventProcessor {
        private float moveLeft, moveRight;
        private IPlayerBuffState playerBuffState;
        public int lives {get; private set;}
        public bool IsDead;
        public IPlayerBuffState PlayerBuffState 
        {
            get
            {
                return playerBuffState;
            }
        set {
            playerBuffState = value;
            Shape.Extent = value.GetExtent();
            }
        }
        public Player(DynamicShape shape, IBaseImage image, IPlayerBuffState buffState)
             : base(shape, image) {
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, this);
            moveLeft = 0.00f;
            moveRight = 0.00f;
            playerBuffState = buffState;
            lives = 4;
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "HandlePowerUp") {
                if (gameEvent.EventType ==  GameEventType.ControlEvent) {
                    switch (PowerUpTransformer.TransformStringToPowerUp(gameEvent.StringArg1)) {
                        case PowerUps.Elongate:
                            PlayerBuffState = new ElongateBuffState();
                            break;
                        case PowerUps.SpeedBuff:
                            PlayerBuffState = new SpeedBuffState();
                            break;
                        default:
                            break;
                    }  
                }
                else if (gameEvent.EventType == GameEventType.TimedEvent) {
                    switch (PowerUpTransformer.TransformStringToPowerUp(gameEvent.StringArg1)) {
                        case PowerUps.SpeedBuff:
                        case PowerUps.Elongate:
                            PlayerBuffState = new RegularBuffState();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        
        //Methods for movement. Render and update is in the entity baseclass
        
        public void Render() {
            RenderEntity();
        }
        
        public void Move() {
            if (GetPosition().X < 0.0f && Shape.AsDynamicShape().Direction.X < 0.01f) {} 
            else if (GetPosition().X > 0.8f && Shape.AsDynamicShape().Direction.X > 0.01f) {}
            else {
                Shape.AsDynamicShape().Move();
            } 
            //Ensure at ændrignen er indenfor intervallet
        }

        public void SetMoveLeft(bool val) {
            if (val) {
                moveLeft = -(playerBuffState.GetSpeed());
            }
            else {
                moveLeft = 0.00f;
            }
            UpdateDirection();
        }

        public void SetMoveRight(bool val) {
            if (val) {
                
                moveRight = playerBuffState.GetSpeed();       
            }      
            else {
                moveRight = 0.00f;
            }
            UpdateDirection(); 
        }

        private void UpdateDirection() {
                Shape.AsDynamicShape().Direction.X = moveLeft + moveRight;
        }

        public Vec2F GetPosition() {
            return Shape.AsDynamicShape().Position;
        }

        public void DecrementLives() {
            if (lives == 1) {
                IsDead = true; 
            }
            else lives--;
        }
    }
} 