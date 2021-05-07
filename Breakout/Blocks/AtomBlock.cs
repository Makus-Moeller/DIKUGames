using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;
using DIKUArcade.Events;


namespace Breakout.Blocks {


    //Superclass which have the basic functionality
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

        public int GetHitpoints() {
            return hitpoints;
        }


        public void HitBlock(int decrementValue) {
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

        public void AddHitpoint(int amount) {
            hitpoints += amount;
        }
        public bool IsHardened() {
            return isHardened;
        }
    }
}