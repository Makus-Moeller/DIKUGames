using System.IO;
using System;
using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout.Levelloader {
    public interface IBlockCreator {
        List<AtomBlock> CreateBlocks(CharDefiners[] charDefiners);
    }
}