using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga;

namespace GalagaStates {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState {get; private set;}
        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            ActiveState = MainMenu.GetInstance();
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = ;
                    break;
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                case GameStateType.MainMenu:
                    return "MAINMENU";
                default:
                    throw new ArgumentException("Not valid State");

                
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}