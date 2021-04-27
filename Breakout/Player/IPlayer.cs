using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Players {
    public interface IPlayerBuffState {
        void Move();
        void SetMoveLeft(bool val);
        void SetMoveRight(bool val);
    }
}