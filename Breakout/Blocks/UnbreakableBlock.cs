using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {
    /// <summary>
    /// Unbreakable block. Can not be hit. 
    /// </summary>
    public class UnbreakableBlock : AtomBlock {
        public UnbreakableBlock(Shape shape, IBaseImage image) : base(shape, image) {
            unbreakable = true;
        }
    }
}