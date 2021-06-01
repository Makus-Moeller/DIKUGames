using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using System.IO;
using DIKUArcade.Utilities;

namespace Breakout {
    
    /// <summary>
    /// An eventhandler that controls the point System
    /// </summary>
    public class Rewards : IGameEventProcessor {
        public int rewards {get; private set;}
        private Text display;
        private Entity scoreImage;
        public Rewards() {
            rewards = 0;
            display= new Text("+" + rewards.ToString(), new Vec2F(0.85f, 0.834f), 
                new Vec2F(0.25f, 0.16f));
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
            scoreImage = new Entity(new StationaryShape(new Vec2F(0.8f, 0.96f), 
                new Vec2F(0.2f, 0.037f)), 
                new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", 
                    "emptyPoint.png")));
        }
        /// <summary>
        /// Ads points
        /// </summary>
        /// <param name="addPoints">points to be added</param>  
        private void AddPoints(int addPoints) {
            rewards += addPoints;
        }

        /// <summary>
        /// handles status Events
        /// and delegates effects to members
        /// </summary>
        /// <param name="gameEvent">The event raised</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch (gameEvent.Message) {
                    case "INCREASE_SCORE":
                        AddPoints(System.Convert.ToInt32(gameEvent.StringArg2));
                        break;
                    default:
                        break;
                }
            }
        }

        public void RenderScore() {
            scoreImage.RenderEntity();
            display.SetText("+" + rewards.ToString());
            display.SetColor(new Vec3I(255, 217, 25));
            display.RenderText();
        }
        public void UpdateScore() {
            display.SetText(rewards.ToString());
        }
    }
}