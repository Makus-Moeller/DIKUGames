using DIKUArcade.Entities;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;
using Breakout.Players;
namespace Breakout.Balls {

    /// <summary>
    /// Stud of CollisionHandler. The iterative methods
    /// Have been removed and the Handle collision made
    /// public and returns integer, based on path.
    /// </summary>
    public class CollisionHandlerStud {

        /// <summary>
        /// Checks if the input has colided with the ball
        /// And delegates outcome to methods that handle 
        /// different cases
        /// </summary>
        /// <param name="comparator">Entity to check collision</param>
        public int HandleCollision(Entity moveable, Entity comparator, CollisionData collisionData) {
            var dynamicDownCast = moveable.Shape.AsDynamicShape();
            var staticDownCast = comparator.Shape;
            if(moveable.Shape.AsDynamicShape().Direction.Y == 0.0f) {
                dynamicDownCast = comparator.Shape.AsDynamicShape();
                staticDownCast = moveable.Shape;
            }
            if(collisionData.Collision) {
                var iscollidable = IsIcollidable(moveable);
                if(iscollidable is NullCollidable) {
                    return 2;
                }
                else {
                    return 1;
                }
            }
            else {
                return 0;
            }
        }
        
        /// <summary>
        /// checks if the entity is an implementation of Icollidable
        /// That is can it collide with others
        /// </summary>
        private ICollidable IsIcollidable(Entity entity) {
            ICollidable collidable = null;
            if ((collidable = (entity as ICollidable)) != null) {
                return collidable;
            } 
            else {
                return new NullCollidable();
            }
        }
    }
}