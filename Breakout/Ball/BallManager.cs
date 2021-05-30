using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Physics;
using DIKUArcade.Events;
using Breakout.PowerUpSpace;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Utilities;

namespace Breakout.Balls {

    /// <summary>
    /// Handles all balls in game.
    /// </summary>
    public class BallManager : IGameEventProcessor {
        public EntityContainer<Ball> allBalls;

        public BallManager() {
            allBalls = new EntityContainer<Ball>();
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, this);
        }
        
        /// <summary>
        /// Process events concerning balls.
        /// </summary>
        /// <param name="gameEvent">The gameevent that should be handled</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "HandlePowerUp") {
                if (gameEvent.EventType ==  GameEventType.ControlEvent) {
                    switch (PowerUpTransformer.TransformStringToPowerUp(gameEvent.StringArg1)) {
                        case PowerUps.Split:
                            TwoDMatrix rotationalMatrix = new TwoDMatrix();
                            allBalls.Iterate(ball => {
                                rotationalMatrix.CreateRoationMatrix(120.0);
                                AddBall(ball.Shape.Position, 
                                    rotationalMatrix.multiplyByVector(ball.Shape.AsDynamicShape().Direction));
                                rotationalMatrix.CreateRoationMatrix(-120.0);
                                AddBall(ball.Shape.Position, 
                                    rotationalMatrix.multiplyByVector(ball.Shape.AsDynamicShape().Direction));
                            });
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        
        /// <summary>
        /// Adds a ball to the ball entitycontainer.
        /// </summary>
        /// <param name="position">The desired position of the object</param>
        /// <param name="objectOfCollision">The direction of the ball</param>
        public void AddBall(Vec2F position, Vec2F direction) {
            allBalls.AddEntity(new Ball(new DynamicShape(position, new Vec2F(0.04f, 0.04f), 
                    direction),
                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "ball2.png"))));
        }
    }
}