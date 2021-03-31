using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;

namespace Galaga
{
    public class Score : IGameEventProcessor<object> {

        public int score {get; private set;}
        private Text display;
        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position, extent);        
            GalagaBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        }

        private void AddPoint(int addPoint) {
            score += addPoint;
        }

        //Implementation of Processevent
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.StatusEvent) {
                switch (gameEvent.Message) {
                    case "INCREASE_SCORE":
                        AddPoint(System.Convert.ToInt32(gameEvent.Parameter2));
                        break;
                    default:
                        break;
               }
            }
        }

        public void RenderScore() {
            display.SetText("Score : " + score.ToString());
            display.SetColor(new Vec3I(191, 0, 255));
            display.RenderText();

        } 
    }
}