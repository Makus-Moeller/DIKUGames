using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Diagnostics.Contracts;
using System;


namespace Breakout.Players {
    public class Player : IPlayer {
        private Entity entity;
        private float moveLeft, moveRight;
        private const float MOVEMENT_SPEED = 0.015f; 

       
        public Player(DynamicShape shape, IBaseImage image) {
            moveLeft = 0.00f;
            moveRight = 0.00f;
            entity = new Entity(shape, image);
        }

        //Methods for movement. Render and update is in the entity baseclass

        public void Render() {
            entity.RenderEntity();
        }
        
        public void Move() {
            if (GetPosition().X < 0.0f && entity.Shape.AsDynamicShape().Direction.X < 0.01f) {} 
            else if (GetPosition().X > 0.8f && entity.Shape.AsDynamicShape().Direction.X > 0.01f) {}
            else {
                entity.Shape.AsDynamicShape().Move();
            } 
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

        private void UpdateDirection() {
                entity.Shape.AsDynamicShape().Direction.X = moveLeft + moveRight;
        }

        private Vec2F GetPosition() {
            return entity.Shape.AsDynamicShape().Position;
        }

    }
} 