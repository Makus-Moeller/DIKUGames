using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;

namespace Breakout.Blocks {
    public class AtomBlock : Entity, IBlocks  {
        private int hitpoints;

        
        public AtomBlock(DynamicShape shape, IBaseImage image) : base(shape, image) {
            hitpoints = 10;
        }

        public int Hitpoints {get;}

        public void CreateBlocks()
        {
            throw new NotImplementedException();
        }

        public int GetHitpoints()
        {
            throw new NotImplementedException();
        }


        public void HitBlock()
        {
            if (hitpoints == 1) {
                DeleteEntity();
            }
            else 
                hitpoints -= 1;
        }

        public int AddHitpoint()
        {
            throw new NotImplementedException();
        }
    }
}