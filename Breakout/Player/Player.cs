using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Diagnostics.Contracts;
using System;


namespace Breakout.Players {
    public class Player : IPlayer {
        
        private Entity entity;
        private DynamicShape shape;

        private float moveLeft, moveRight;
        private const float MOVEMENT_SPEED = 0.015f; 

       
        public Player(DynamicShape shape, IBaseImage image) {
            moveLeft = 0.00f;
            moveRight = 0.00f;
            entity = new Entity(shape, image);
            this.shape = shape;

        }

        //Methods for movement. Render and update is in the entity baseclass

        public void Render() {
            entity.RenderEntity();
        }
        
        public void Move() {
            if (GetPosition().X < 0.0f && shape.Direction.X < 0.01f) {} 
            else if (GetPosition().X > 1.0f && shape.Direction.X > 0.01f) {}
            else {
                shape.Move();
            } 
        }
        


        public void SetMoveLeft(bool val) {
            if (val) {
                Console.WriteLine(shape.Position.X);
                moveLeft = -MOVEMENT_SPEED;
            }
            else {
                moveLeft = 0.00f;
            }
            UpdateDirection();
        }    
        public void SetMoveRight(bool val) {
            if (val) {
                Console.WriteLine(shape.Position.X);
                moveRight = MOVEMENT_SPEED;               
            }      
            else {
                moveRight = 0.00f;
            }
            UpdateDirection();
            
            
        }
        public void UpdateDirection() {
            Console.WriteLine("Før if-statements");
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