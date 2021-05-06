using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {

    public class HardenedBlock : AtomBlock {
        
        public HardenedBlock(Shape shape, IBaseImage image) : base(shape, image) {
            isHardened = true;
            hitpoints += 4; 
            value += 1;
        }
    }
}