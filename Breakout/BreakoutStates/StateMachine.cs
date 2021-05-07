using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;
using Breakout;
using System;

namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState {get; private set;}
        public StateMachine() {
            ActiveState = MainMenu.GetInstance();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
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
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                    case "CHANGE_STATE":
                        SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1), 
                                gameEvent.StringArg2);
                        break;
                    default:
                        break;
                }
            }
            else if (gameEvent.EventType == GameEventType.InputEvent) {
                KeyboardAction action = KeyboardAction.KeyPress;
                KeyboardKey key;
                switch (gameEvent.Message) {
                    case "KEY_UP":
                        key = KeyboardKey.Up;
                        break;
                    case "KEY_DOWN":
                        key = KeyboardKey.Down;
                        break;
                    case "ENTER":
                        key = KeyboardKey.Enter;
                        break;
                    case "KEY_LEFT":
                        key = KeyboardKey.Left;
                        break;
                    case "KEY_RIGHT":
                        key = KeyboardKey.Right;
                        break;
                    case "KEY_LEFT_RELEASED":
                        key = KeyboardKey.Left;
                        action = KeyboardAction.KeyRelease;
                        break;
                    case "KEY_RIGHT_RELEASED":
                        key = KeyboardKey.Right;
                        action = KeyboardAction.KeyRelease;
                        break;
                    case "ESCAPE":
                        key = KeyboardKey.Escape;
                        break;
                    default:
                        key = KeyboardKey.Unknown;
                        break;
                }
                ActiveState.HandleKeyEvent(action, key);
            }
        }
    }
}