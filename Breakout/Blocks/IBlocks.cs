using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public interface IBlocks {
        int GetHitpoints();

        void AddHitpoint(int amount);
    
    }
}