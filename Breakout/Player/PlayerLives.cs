using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.Players {
    
    /// <summary>
    /// An eventhandler that controls the point System
    /// </summary>
    public class PlayerLives {
        public int lives {get; private set;}
        private Text display;
        private Vec2F placement;
        private Vec2F width;
        private Player player;
        
    
        public PlayerLives (Vec2F position, Vec2F extent, Player player) {
            this.player = player;
            lives = player.lives;
            placement = position;
            width = extent;
            display= new Text("Lives: " + lives, placement, width);
        }

        public void RenderLives() {
            display.SetText("Lives : " + lives.ToString());
            display.SetColor(new Vec3I(191, 0, 255));
            display.RenderText();
        }
        public void UpdateLives() {
            lives = player.lives;
            display= new Text("Lives: " + lives, placement, width);
            RenderLives();
        }
    }
}