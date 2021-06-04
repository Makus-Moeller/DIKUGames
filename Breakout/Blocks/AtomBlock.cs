using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using Breakout.Players;
using Breakout.Balls;

namespace Breakout.Blocks {
    /// <summary>
    /// Superclass of blocks which have the basic functionality
    /// </summary>
    public class AtomBlock : Entity, ICollidable {
        protected int hitpoints;
        protected bool unbreakable;
        protected bool isHardened;
        protected int value;

        public AtomBlock(Shape shape, IBaseImage image) : base(shape, image) {
            hitpoints = 10;
            value = 1;
        }

        /// <summary>
        /// Hit block with certain amount
        /// </summary>
        /// <param name="decrementValue">Amount a block is hit by</param>
        public virtual void HitBlock(int decrementValue) {
            if (!unbreakable) {
                if ((hitpoints -= decrementValue) < 1) {
                    this.DeleteEntity();
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{EventType = 
                        GameEventType.StatusEvent,
                        Message = "INCREASE_SCORE",
                        StringArg2 = value.ToString(),
                        });
                }
            }
        }


        public void HandleThisCollision(CollisionData data, Entity objectOfCollision) {
            IDamager damager;
            if ((damager = objectOfCollision as IDamager) != null) {
                HitBlock(damager.DamageOfObject());
            }
        }

        //Method used for testing        
        public int GetHitpoints() {
            return hitpoints;
        }

        //Method used for testing        
        public bool IsHardened () {
            return isHardened;
        }
    }
}