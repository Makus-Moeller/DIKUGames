using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.EventBus;

namespace Galaga {
    public class Score : IGameEventProcessor<object> {

        private int score;
        private Text display;
        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position, extent);        
        }

        public void AddPoint() {
            score++;
           
        }

        //Implementation of Processevent
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                    case "INCREASE_SCORE":
                        AddPoint();
                        break;
               }
            }
        }

        public void RenderScore() {
            display.SetText(score.ToString());
            display.SetColor(new Vec3I(191, 0, 255));
            display.RenderText();

        } 
    }
}