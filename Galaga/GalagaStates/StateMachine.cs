using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga;
using System;
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
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    break;
                case GameStateType.GameRunning:
                    break;
                default:
                    throw new ArgumentException("Not valid State");

                
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            throw new System.NotImplementedException();
            //SwitchState(gameEvent.message.statetostring())
        }
    }
}