using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;

namespace Galaga {
    public class Player : IGameEventProcessor<object> {
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
            shape.Move();   
        }

        private void SetMoveLeft(bool val) {
            if (val) {
                moveLeft -= 0.01f;
            }
            else {
                moveLeft = 0.00f;
            }
            UpdateDirection();
        }    
        private void SetMoveRight(bool val) {
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

        //Implementer Processevent
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case "KEY_LEFT":
                        SetMoveLeft(true);
                        break;
                    case "KEY_RIGHT":
                        SetMoveRight(true);
                        break;
                    case "KEY_LEFT_RELEASED":
                        SetMoveLeft(false);
                        break;
                    case "KEY_RIGHT_RELEASED":
                        SetMoveRight(false);
                        break;
                }
            }
        }
    }
}
