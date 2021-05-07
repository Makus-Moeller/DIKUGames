using DIKUArcade.Physics;
using DIKUArcade.Entities;
namespace Breakout.Players {
    
    public interface IBall {
        void MoveBall();
        void UpdateBall(Entity comparator, Player player);
    }

}