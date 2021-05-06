using Breakout;
using System;

namespace Breakout.BreakoutStates {
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
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                case "MAINMENU":
                    return GameStateType.MainMenu;
                default:
                    throw new ArgumentException("Not valid State"); 
            }
        }      

        public static string TransformStateToString(GameStateType state) {
            switch (state) {
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                case GameStateType.MainMenu:
                    return "MAINMENU";
                default:
                    throw new ArgumentException("Not valid State");
            }
        }
    }
}