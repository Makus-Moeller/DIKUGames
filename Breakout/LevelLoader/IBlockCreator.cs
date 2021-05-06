using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Entities;
namespace Breakout.Levelloader
{
    public interface IBlockCreator {
        EntityContainer<AtomBlock> CreateBlocks(CharDefiners[] charDefiners);
    }
}