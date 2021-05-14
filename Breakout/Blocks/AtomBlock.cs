using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;
using DIKUArcade.Events;


namespace Breakout.Blocks {


    
    /// <summary>
    /// Superclass of blocks which have the basic functionality
    /// </summary>
    public class AtomBlock : Entity {
        protected int hitpoints;
        protected bool unbreakable;
        protected bool isHardened;
        protected Entity PowerUpItem;
        protected int value;

        public AtomBlock(Shape shape, IBaseImage image) : base(shape, image) {
            hitpoints = 10;
            value = 1;
        }
        
        /// <summary>
        /// Returns a blockobjects hitpoints
        /// </summary>
        public int GetHitpoints() {
            return hitpoints;
        }

        /// <summary>
        /// Hit block with certain amount
        /// </summary>
        /// <param name="decrementValue">Amount a block is hit by</param>
        public virtual void HitBlock(int decrementValue) {
            if (!unbreakable) {
                if ((hitpoints -= decrementValue) < 1) {
                    this.DeleteEntity();
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{EventType = GameEventType.StatusEvent,
                        Message = "INCREASE_SCORE",
                        StringArg2 = value.ToString(),
                        });
                }
            }
        }

        /// <summary>
        /// Add hitpoints to block with certain amount
        /// </summary>
        /// <param name="amount">Amount hitpoints increased</param>
        public void AddHitpoint(int amount) {
            hitpoints += amount;
        }
        
        public bool IsHardened() {
            return isHardened;
        }
    }
}