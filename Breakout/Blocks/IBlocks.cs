namespace Breakout.Blocks {

    /// <summary>
    /// Interface alle blocks have to implement.
    /// </summary>
    public interface IBlocks {
        int HitPoints {
            get;
        } 
        int GetHitpoints();

        void AddHitpoint(int amount);
    }
}