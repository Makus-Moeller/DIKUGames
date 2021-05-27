using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Timers;
using System;

namespace Breakout.BreakoutStates {

    /// <summary>
    /// Processes different gameevents and controls the game flow.
    /// switches between states when nessecary.
    /// </summary>
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState {get; private set;}
        public StateMachine() {
            ActiveState = MainMenu.GetInstance();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        }

        /// <summary>
        /// Switches between states.
        /// </summary>
        /// <param name="stateType">What state should be active</param>
        /// <param name="sender">who sends the gamestateevent</param>
        public void SwitchState(GameStateType stateType, string sender) {
            switch (stateType) {
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.GameRunning:
                    //Hvis afsender er MainMenu skal den genstarte spillet
                    if (sender == "MAINMENU") {
                        StaticTimer.RestartTimer();
                        GameRunning.GetInstance().InitializeGameState();
                    }
                    StaticTimer.ResumeTimer();
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    StaticTimer.PauseTimer();
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.GameWon:
                    ActiveState = GameWon.GetInstance();
                    break;
                case GameStateType.GameLost:
                    ActiveState = GameLost.GetInstance();
                    break;
                default:
                    throw new ArgumentException("Not valid State");
            }
        }

        /// <summary>
        /// Process gameevents that class is subscribed to.
        /// </summary>
        /// <param name="gamevent">GameEvent that should be processed</param>
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
                    case "KEY_SPACE_RELEASED":
                        key = KeyboardKey.Space;
                        action = KeyboardAction.KeyRelease;
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