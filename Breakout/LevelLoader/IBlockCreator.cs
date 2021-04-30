using System.Collections.Generic;
using Breakout.Blocks;

namespace Breakout.Levelloader
{
    public interface IBlockCreator {
        List<AtomBlock> CreateBlocks(CharDefiners[] charDefiners);
    }
}