using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;

namespace Breakout.Blocks {
    public class UnbreakableBlock : AtomBlock {
        public UnbreakableBlock(Shape shape, IBaseImage image) : base(shape, image) {
            unbreakable = true;
        }
    }
}