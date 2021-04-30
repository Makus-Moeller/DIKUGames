using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {

    public class HardenedBlock : AtomBlock {
        private bool isHardened;

        public HardenedBlock(Shape shape, IBaseImage image) : base(shape, image) {
            isHardened = true;
            hitpoints += 4; 
        }

        public bool IsHardened() {
            return isHardened;
        }
    }
}