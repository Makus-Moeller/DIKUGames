using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;

namespace Breakout.Players {
    public class Player : Entity, ICollidable {
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

            moveLeft = 0.00f;
            moveRight = 0.00f;
            playerBuffState = buffState;
            lives = 2;
        }
        
        public void HandleThisCollision(CollisionData data, Entity objectOfCollision) {
        /*
            var powerUpItem = objectOfCollision as PowerUp;
            if ((muligvis = objectOfCollision as PowerUp) != null) {
                switch (powerUpItem.PowerUp) {
                    case(PowerUpTypes.Elongate):
                        playerBuffState = new ElongateBuffState();
                        break;
                    default:
                        break;
                }
            }
            */
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
                moveLeft = -(playerBuffState.Getspeed());
            }
            else {
                moveLeft = 0.00f;
            }
            UpdateDirection();
        }

        public void SetMoveRight(bool val) {
            if (val) {
                
                moveRight = playerBuffState.Getspeed();       
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
            if (lives == 0) {
                IsDead = true; 
            }
            else lives--;
        }
    }
} 