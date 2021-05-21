using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout {
    
    /// <summary>
    /// An eventhandler that controls the point System
    /// </summary>
    public class Rewards : IGameEventProcessor {
        public int rewards {get; private set;}
        private Text display;
        private Vec2F placement;
        private Vec2F width;
        public Rewards (Vec2F position, Vec2F extent) {
            rewards = 0;
            placement = position;
            width = extent;
            display= new Text("Score: " + rewards, placement, width);
            BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
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
            display.SetText("Score : " + rewards.ToString());
            display.SetColor(new Vec3I(191, 0, 255));
            display.RenderText();
        }
        public void UpdateScore() {
            display= new Text("Score: " + rewards, placement, width);
        }
    }
}