using DIKUArcade.Entities;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;

namespace Breakout.Balls {

    /// <summary>
    /// Decides what the outcome of a collision should be
    /// </summary>
    public class CollisionHandler {

        /// <summary>
        /// Checks if the input has colided with the ball
        /// And delegates outcome to methods that handle 
        /// different cases
        /// </summary>
        /// <param name="comparator">Entity to check collision</param>
        private void HandleCollision(Entity moveable, Entity comparator) {
            var dynamicDownCast = moveable.Shape.AsDynamicShape();
            var staticDownCast = comparator.Shape;
            if(moveable.Shape.AsDynamicShape().Direction.Y == 0.0f) {
                dynamicDownCast = comparator.Shape.AsDynamicShape();
                staticDownCast = moveable.Shape;
            }
            CollisionData collisiondata = CollisionDetection.Aabb(dynamicDownCast, staticDownCast);
            if(collisiondata.Collision) {
                IsIcollidable(moveable).HandleThisCollision(collisiondata, comparator);
                IsIcollidable(comparator).HandleThisCollision(collisiondata, moveable);
            }
        }

        /// <summary>
        /// simply checks all possible coliders
        /// </summary>
        public void HandleEntityCollisions<T>(Entity collidingEntity, 
            EntityContainer<T> AllpossibleCollisions) where T: Entity { 
                AllpossibleCollisions.Iterate(entity => HandleCollision(collidingEntity, entity));
        }

        /// <summary>
        /// Checks both multiple colliding entities and multiple coliders
        /// IE. The same as HandleEntityCollisions but iterates over the 
        /// first argument as well.
        /// </summary>
        public void HandleMultiEntityCollisions<T, S>(EntityContainer<T> collidingEntities, 
            EntityContainer<S> AllpossibleCollisions) where T : Entity where S : Entity {
                collidingEntities.Iterate(collidingEntity => 
                    AllpossibleCollisions.Iterate(entity => 
                        HandleCollision(collidingEntity, entity)));
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