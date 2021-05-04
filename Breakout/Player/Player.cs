using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


namespace Breakout.Players {
    public class Player : Entity {
        private float moveLeft, moveRight;
        private const float MOVEMENT_SPEED = 0.015f; 
        private IPlayerBuffState playerBuffState;
        public IPlayerBuffState PlayerBuffState 
        {
            get
            {
                return playerBuffState;
            }
        set {
            value.AddBuffs(this);
            playerBuffState = value;
            }
        }
        public Player(DynamicShape shape, IBaseImage image, IPlayerBuffState buffState) : base(shape, image) {

            moveLeft = 0.00f;
            moveRight = 0.00f;
            playerBuffState = buffState;
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

    }
} 