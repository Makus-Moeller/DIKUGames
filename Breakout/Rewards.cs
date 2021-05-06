using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System;
namespace Breakout 
{
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
            
        private void AddPoints(int addPoints) {
            rewards += addPoints;
            

        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                Console.WriteLine("fanger den event");
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