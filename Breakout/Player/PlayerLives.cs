using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.Players {
    
    /// <summary>
    /// An eventhandler that controls the point System
    /// </summary>
    public class PlayerLives {
        public int Lives {get; private set;}
        private Text display;
        private Vec2F placement;
        private Vec2F width;
        
    
        public PlayerLives (Vec2F position, Vec2F extent) {
            Lives = 4;
            placement = position;
            width = extent;
            display = new Text("Lives: " + Lives.ToString(), placement, width);
        }

        public void addLife() {
            Lives++;
        }

        public void DecrementLives() {
            Lives--;
        }
        public void RenderLives() {
            display.SetColor(new Vec3I(191, 0, 255));
            display.RenderText();
        }
        public void UpdateLives() {
            display.SetText("Lives: " + Lives.ToString()); 
            RenderLives();
        }
    }
}