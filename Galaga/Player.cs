using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Player {
        private Entity entity;
        private DynamicShape shape;
        private float moveLeft, moveRight;
        private const float MOVEMENT_SPEED = 0.01f;
        public Player(DynamicShape shape, IBaseImage image) {
            moveLeft = 0.00f;
            moveRight = 0.00f;
            entity = new Entity(shape, image);
            this.shape = shape;
        } 
        public void Render() {
            entity.RenderEntity();
        }
        public void Move() {
            //Todo: Move the shape and guard against the window boarders
            shape.Move();   
        }

        public void SetMoveLeft(bool val) {
            if (val) {
                moveLeft -= 0.01f;
            }
            else {
                moveLeft = 0.00f;
            }
            UpdateDirection();
        }    
        public void SetMoveRight(bool val) {
            if (val) {
                moveRight += 0.01f;               
            }      
            else {
                moveRight = 0.00f;
            }
            UpdateDirection();
            
            
        }
        private void UpdateDirection() {
            if (shape.Position.X < 0.0f) {
                shape.Direction.X = moveRight;
            }
            else if (shape.Position.X > 0.9f) {
                shape.Direction.X = moveLeft;
            }
            else {
                shape.Direction.X = moveLeft + moveRight;
            }
        }

        public Vec2F getPosiiton() {
            return shape.Position;
        }
    }
}
