using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Utilities;
using DIKUArcade.Entities;

namespace Breakout.Players {
    
    /// <summary>
    /// An eventhandler that controls the point System
    /// </summary>
    public class PlayerLives {
        public int Lives {get; private set;}
        
        public PlayerLives (Vec2F position, Vec2F extent) {
            Lives = 4;
        }

        public void addLife() {
            Lives++;
        }

        public void DecrementLives() {
            Lives--;
        }
        public void RenderLives() {
            float Xposition = 0.02f;
            for (int i = 0; i < Lives; i++) {
                Entity heart = new Entity(new StationaryShape(new Vec2F(Xposition, 0.97f), 
                    new Vec2F(0.1f, 0.022f)), 
                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", 
                        "heart_filled.png")));
                heart.RenderEntity();
                Xposition += 0.08f;
            } 
        }
        public void UpdateLives() {
            RenderLives();
        }
    }
}