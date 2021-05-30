using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;
using DIKUArcade.Physics;

namespace Breakout.Balls {
    
    /// <summary>
    /// The implementation of a moveable Ball
    /// Ball is in charge of moving itself 
    /// correctly
    /// </summary>
    public class Ball : Entity, ICollidable {
        public readonly double speedOfBall;

        public Ball(Shape shape, IBaseImage image) : base(shape, image) {
            var dyshape = Shape.AsDynamicShape();
            speedOfBall = Math.Sqrt(Math.Pow(dyshape.Direction.X, 2.00f) + 
                Math.Pow(dyshape.Direction.Y, 2.00f));
        }

        /// <summary>
        /// Ensures that ball changes direction if it
        /// Breaches the boundris of the map
        /// </summary>
        public void HitWall() {
            if (Shape.Position.X > 0.98f && Shape.Position.Y > 0.98f) {
                Shape.AsDynamicShape().Direction.X = (float) -((speedOfBall*Math.Sqrt(2.0))/2.0);
                Shape.AsDynamicShape().Direction.Y = (float) -((speedOfBall*Math.Sqrt(2.0))/2.0);
            }
            else if (Shape.Position.X < 0.02f && Shape.Position.Y > 0.98f) {
                Shape.AsDynamicShape().Direction.X = (float) ((speedOfBall*Math.Sqrt(2.0))/2.0);
                Shape.AsDynamicShape().Direction.Y = (float) -((speedOfBall*Math.Sqrt(2.0))/2.0);
            }
            else if (Shape.Position.X > 0.98f) {
                Shape.AsDynamicShape().Direction.X = -Shape.AsDynamicShape().Direction.X;
            }
            else if (Shape.Position.X < 0.00f) {
                Shape.AsDynamicShape().Direction.X = -Shape.AsDynamicShape().Direction.X;
            }
            else if (Shape.Position.Y > 0.97f) {
                Shape.AsDynamicShape().Direction.Y = -Shape.AsDynamicShape().Direction.Y;
            }
            else if (Shape.Position.Y < 0.01f) {
                this.DeleteEntity();
            }
        }

        /// <summary>
        /// uses trigonomitry to calculate new direction vector.
        /// Note: this will not change speed of ball
        /// </summary>
        /// <param name="data">Contains the details of collision</param>
        /// <param name="objectOfCollision">Entity that colided with ball</param>
        public void HandleThisCollision(CollisionData data, Entity objectOfCollision) {
            TwoDMatrix matrix = new TwoDMatrix();
            switch (data.CollisionDir) {
                case (CollisionDirection.CollisionDirDown):
                    ///Hvis bevæger sig mod højre
                    if(Shape.AsDynamicShape().Direction.X > 0.0f) {
                        ///Hvis den rammer på halvdelen tættest på sig selv
                        if(Shape.Position.X < (objectOfCollision.Shape.Position.X + 
                            (objectOfCollision.Shape.Extent.X / 2.0f))) {
                                matrix.CreateRoationMatrix(-((180.0 - 2*(90.0 - 
                                (Math.Acos(Shape.AsDynamicShape().Direction.X / speedOfBall)) 
                                * 180.0/Math.PI))) - 10);
                        } 
                        ///Hvis den rammer på halvdelen længst på sig selv
                        else {
                            matrix.CreateRoationMatrix(-((180.0 - 2*(90.0 - 
                            (Math.Acos(Shape.AsDynamicShape().Direction.X / speedOfBall)) * 
                            180.0/Math.PI))) + 10);
                        }
                        Shape.AsDynamicShape().Direction = 
                            matrix.multiplyByVector(Shape.AsDynamicShape().Direction);
                    } 
                    ///Hvis den bevæger sig mod venstre
                    else {
                        ///Hvis den rammer på halvdelen tættest på sig selv
                        if(Shape.Position.X > (objectOfCollision.Shape.Position.X + 
                            (objectOfCollision.Shape.Extent.X / 2.0f))) {
                                matrix.CreateRoationMatrix(((180.0 - 2*(90.0 - 
                                    (Math.Acos(-Shape.AsDynamicShape().Direction.X / speedOfBall)) * 
                                    180.0/Math.PI))) + 10);
                        } 
                        ///Hvis den rammer på halvdelen længst på sig selv
                        else {
                            matrix.CreateRoationMatrix(((180.0 - 2*(90.0 - 
                                (Math.Acos(-Shape.AsDynamicShape().Direction.X / speedOfBall)) * 
                                180.0/Math.PI))) - 10);
                        }
                        Shape.AsDynamicShape().Direction = 
                            matrix.multiplyByVector(Shape.AsDynamicShape().Direction);
                    }   
                    break;
                case (CollisionDirection.CollisionDirUp):
                    ///Hvis bevæger sig mod højre
                    if(Shape.AsDynamicShape().Direction.X > 0.0f) {
                        ///Hvis den rammer på halvdelen tættest fra sig selv
                        if(Shape.Position.X < (objectOfCollision.Shape.Position.X + 
                            (objectOfCollision.Shape.Extent.X / 2.0f))) {
                                matrix.CreateRoationMatrix(((180.0 - 2*(90.0 - 
                                    (Math.Acos(Shape.AsDynamicShape().Direction.X / speedOfBall)) * 
                                    180.0/Math.PI))) + 10);
                        } 
                        ///Hvis den rammer på halvdelen længst på sig selv
                        else {
                            matrix.CreateRoationMatrix(((180.0 - 2*(90.0 - 
                                (Math.Acos(Shape.AsDynamicShape().Direction.X / speedOfBall)) * 
                                180.0/Math.PI))) - 10);
                        }
                        Shape.AsDynamicShape().Direction = 
                            matrix.multiplyByVector(Shape.AsDynamicShape().Direction);
                    }
                    ///Hvis den bevæger sig mod venstre
                    else {
                        ///Hvis den rammer på halvdelen tættest fra sig selv
                        if(Shape.Position.X > (objectOfCollision.Shape.Position.X +
                             (objectOfCollision.Shape.Extent.X / 2.0f))) {
                                matrix.CreateRoationMatrix(-((180.0 - 2*(90.0 - 
                                    (Math.Acos(-Shape.AsDynamicShape().Direction.X / speedOfBall)) * 
                                    180.0/Math.PI))) - 10);
                        } 
                        ///Hvis den rammer på halvdelen længst på sig selv
                        else {
                            matrix.CreateRoationMatrix(-((180.0 - 2*(90.0 - 
                                (Math.Acos(-Shape.AsDynamicShape().Direction.X / speedOfBall)) * 
                                180.0/Math.PI))) + 10);
                        }
                        Shape.AsDynamicShape().Direction = 
                            matrix.multiplyByVector(Shape.AsDynamicShape().Direction);
                    }   
                    break;
                case (CollisionDirection.CollisionDirRight):
                case (CollisionDirection.CollisionDirLeft):
                    Shape.AsDynamicShape().Direction.X *= -1.0f;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// uses the dynamic shape of Entity
        /// to move the ball along direction vector
        /// </summary>
        public void MoveBall() {
            if (!IsDeleted()) {
                Shape.AsDynamicShape().Move();
                HitWall();
            }
        }

        public void RenderBall() {
            if (!IsDeleted()) {
                RenderEntity();
            }
        }
    }
}