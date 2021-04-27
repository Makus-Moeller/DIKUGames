using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Diagnostics.Contracts;
using System;


namespace Breakout.Players  {
    public class RegularBuffState : IPlayerBuffState {
        private const float MOVEMENT_SPEED = 0.015f;
        private Vec2F EXTENT = (new Vec2F(0.2f, 0.03f));
        public float Getspeed()
        {
            return MOVEMENT_SPEED;
        }

        public void AddBuffs(Player player)
        {

        }
    }
}