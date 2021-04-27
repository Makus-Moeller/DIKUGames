using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Diagnostics.Contracts;


namespace Breakout.Players {
    public class Player : Entity, IPlayer {
        private DynamicShape shape;
        private float moveLeft, moveRight;
        private const float MOVEMENT_SPEED = 0.015f;
        public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
            moveLeft = 0.00f;
            moveRight = 0.00f;
            this.shape = shape;
        }

        [ContractInvariantMethod]
        private void ClassInvariant() {
            Contract.Invariant(shape.Position.X > 0.0f && shape.Position.X < 1.0f, "Shape posiiton x has to be betweeen 0 and 1");
        }
        //Methods for movement. Render and update is in the entity baseclass

        public void Move() {
            shape.Move();   
        }

        public void SetMoveLeft(bool val) {
            if (val) {
                moveLeft = -MOVEMENT_SPEED;
            }
            else {
                moveLeft = 0.00f;
            }
            UpdateDirection();
        }    
        public void SetMoveRight(bool val) {
            if (val) {
                moveRight = MOVEMENT_SPEED;               
            }      
            else {
                moveRight = 0.00f;
            }
            UpdateDirection();
            
            
        }
        public void UpdateDirection() {
            if (shape.Position.X < 0.05f) {
                shape.Direction.X = moveRight;
            }
            else if (shape.Position.X > 0.85f) {
                shape.Direction.X = moveLeft;
            }
            else {
                shape.Direction.X = moveLeft + moveRight;
            }
        }

        public Vec2F GetPosition() {
            return shape.Position;
        }

    }
} 