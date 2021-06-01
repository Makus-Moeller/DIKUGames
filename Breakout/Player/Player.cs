using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using Breakout.PowerUpSpace;
using DIKUArcade.Timers;

namespace Breakout.Players {
    /// <summary>
    /// A player classe with different attributes.
    /// </summary>
    public class Player : Entity, IGameEventProcessor {
        private float moveLeft, moveRight;
        private IBuffState playerBuffState;
        public PlayerLives playerLives {get; private set;}
        public bool IsDead;
        public ShotsWeapon Weapon {get; private set;} 
        public IBuffState PlayerBuffState 
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

        public Player(DynamicShape shape, IBaseImage image, IBuffState buffState)
             : base(shape, image) {
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, this);
            moveLeft = 0.00f;
            moveRight = 0.00f;
            playerBuffState = buffState;
            playerLives = new PlayerLives(new Vec2F(0.03f, 0.01f), new Vec2F(0.2f, 0.2f));
            Weapon = new ShotsWeapon();
        }

        /// <summary>
        /// Process gameevents that class is subscribed to.
        /// </summary>
        /// <param name="gamevent">GameEvent that should be processed</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "HandlePowerUp") {
                if (gameEvent.EventType ==  GameEventType.ControlEvent) {
                    switch (PowerUpTransformer.TransformStringToPowerUp(gameEvent.StringArg1)) {
                        case PowerUps.Elongate:
                            if (PlayerBuffState is ElongateBuffState){
                                BreakoutBus.GetBus().ResetTimedEvent(1, TimePeriod.NewSeconds(10.0));
                            }
                            else {
                                PlayerBuffState = new ElongateBuffState();
                            }
                            break;
                        case PowerUps.SpeedBuff:
                            if (PlayerBuffState is SpeedBuffState) {
                                BreakoutBus.GetBus().ResetTimedEvent(1, TimePeriod.NewSeconds(10.0));
                            }
                            else {
                                PlayerBuffState = new SpeedBuffState();
                            }
                            break;
                        case PowerUps.ExtraLife:
                            if (playerLives.Lives > 5) {}
                            else {playerLives.addLife();}
                            break;
                        case PowerUps.Laser:
                            if (Weapon.Active) {
                                BreakoutBus.GetBus().ResetTimedEvent(3, TimePeriod.NewSeconds(10.0));
                            }
                            else {
                                Weapon = new ShotsWeapon();
                                Weapon.Active = true;
                            }
                            break;
                        default:
                            break;
                    }  
                }
                else if (gameEvent.EventType == GameEventType.TimedEvent) {
                    switch (PowerUpTransformer.TransformStringToPowerUp(gameEvent.StringArg1)) {
                        case PowerUps.SpeedBuff:
                            if (PlayerBuffState is SpeedBuffState) {
                                PlayerBuffState = new RegularBuffState();
                            }
                            break;
                        case PowerUps.Elongate:
                            if (PlayerBuffState is ElongateBuffState) {
                                PlayerBuffState = new RegularBuffState();
                            }
                            break;
                        case PowerUps.Laser:
                            Weapon.Active = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        
        //Methods for movement. Render and update is in the entity baseclass
        
        public void Render() {
            playerLives.RenderLives();
            RenderEntity();
            Weapon.Render();
        }
        
        /// <summary>
        /// Move player unless the movement violates window boundary.
        /// </summary>
        public void Move() {
            if (GetPosition().X < 0.0f && Shape.AsDynamicShape().Direction.X < 0.01f) {} 
            else if (GetPosition().X > 0.8f && Shape.AsDynamicShape().Direction.X > 0.01f) {}
            else {
                Shape.AsDynamicShape().Move();
            }
            playerLives.UpdateLives();
            Weapon.Update();
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
        
        /// <summary>
        /// Decrement players lives.
        /// </summary>
        public void DecrementLives() {
            playerLives.DecrementLives();
            if (playerLives.Lives == 0)
                IsDead = true; 
        }

        public void Shoot(Vec2F position, Vec2F extent) {
            Weapon.Fire(position, extent);
        }
    }
} 