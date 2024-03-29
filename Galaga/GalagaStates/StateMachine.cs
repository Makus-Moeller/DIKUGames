using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga;
using System;

namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState {get; private set;}
        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
        }

        public void SwitchState(GameStateType stateType, string sender) {
            switch (stateType) {
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.GameRunning:
                    //Hvis afsender er MainMenu skal den genstarte spillet
                    if (sender == "MAINMENU") {
                        GameRunning.GetInstance().InitializeGameState();
                    }
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                default:
                    throw new ArgumentException("Not valid State");
            }
        }

        //Vi Laver if statements for at tjekke eventypen og switcher for at tjekke præcis hvad den skal gøre
        //Input event skal altid kunne håndteres af HandleKeyEvent så ingen grund til at switche. 
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                    case "CHANGE_STATE":
                        SwitchState(StateTransformer.TransformStringToState(gameEvent.Parameter1), 
                                (gameEvent.Parameter2));
                        break;
                    default:
                        break;
                }
            }
            else if (eventType == GameEventType.InputEvent) {
                ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
            }
        }
    }
}