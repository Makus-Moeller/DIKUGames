using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;
namespace Breakout.Blocks {


    //Superclass which have the basic functionality
    public class AtomBlock : Entity {
        protected int hitpoints;
        protected bool unbreakable;
        protected bool isHardened;
        protected Entity PowerUpItem;

        public AtomBlock(Shape shape, IBaseImage image) : base(shape, image) {
            hitpoints = 10;
        }

        public int HitPoints {get;}
        public int GetHitpoints() {
            return hitpoints;
        }


        public void HitBlock(int decrementValue) {
            if (!unbreakable) {
                if ((hitpoints -= decrementValue) < 1) {
                    this.DeleteEntity();
                    Console.WriteLine("HEr");
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