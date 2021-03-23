using Galaga;
using System;

namespace GalagaStates {
    public enum GameStateType {
        GameRunning, 
        GamePaused,
        MainMenu
    }

    public static class StateTransformer {
        public static GameStateType TransformStringToState(string state) {
            switch (state) {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                    break;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                    break;
                case "MAINMENU":
                    return GameStateType.MainMenu;
                    break;
                default:
                    throw new ArgumentException("Not valid State"); 
            }      

        }
        public static string TransformStateToString(GameStateType state) {
            switch (state) {
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                    break;
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                    break;
                case GameStateType.MainMenu:
                    return "MAINMENU";
                    break;
                default:
                    throw new ArgumentException("Not valid State");
            }
        }
    }
}

