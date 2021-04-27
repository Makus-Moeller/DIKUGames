using DIKUArcade.Entities;

namespace Breakout.Players {
    public interface IPlayerBuffState {
        //void ChangeSpeed();
        void ChangeExtent(Shape shape);
        float Getspeed();
        
    }

}
