using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;

namespace Breakout.Blocks {

    //Superclass which have the basic functionality
    public class AtomBlock : Entity, IBlocks  {
        protected int hitpoints;
        protected bool unbreakable;

        public AtomBlock(Shape shape, IBaseImage image) : base(shape, image) {
            hitpoints = 10;
        }

        public int GetHitpoints()
        {
            return hitpoints;
        }


        public void HitBlock()
        {
            if (!unbreakable) {
                if (hitpoints == 1) {
                    DeleteEntity();
                }
                else 
                    hitpoints -= 1;
           }
        }

        public void AddHitpoint(int amount)
        {
            hitpoints += amount;
        }
    }
}