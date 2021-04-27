using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Diagnostics.Contracts;
using System;


namespace Breakout.Players {
    public class Player : Entity{
        private float moveLeft, moveRight;
<<<<<<< HEAD
        private const float MOVEMENT_SPEED = 0.015f; 
        private IPlayerBuffState playerBuffState {get; set;}

       
        public Player(DynamicShape shape, IBaseImage image) {
=======
        public IPlayerBuffState playerBuffState {get; set;}
        public Player(DynamicShape shape, IBaseImage image, IPlayerBuffState buffState) : base(shape, image){
>>>>>>> cd9f335f7b175ec37d3b8a100f0ed04dfc39a4c8
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

        private Vec2F GetPosition() {
            return Shape.AsDynamicShape().Position;
        }

    }
} 