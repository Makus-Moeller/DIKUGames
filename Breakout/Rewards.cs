using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout 
{
    public class Rewards : IGameEventProcessor {
        public int rewards {get; private set;}
        private Text display;
        public Rewards (Vec2F position, Vec2F extent) {
            rewards = 1;
            display = new Text(rewards.ToString(), position, extent);
        }
        private void AddPoints(int addPoints) {
            rewards += addPoints;
        }
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
    }
}