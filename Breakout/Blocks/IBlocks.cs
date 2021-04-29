namespace Breakout.Blocks
{
    public interface IBlocks {
        int HitPoints {
            get;
        }
        int GetHitpoints();

        void AddHitpoint(int amount);
    }
}