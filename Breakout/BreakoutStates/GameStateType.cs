using System;

namespace Breakout.BreakoutStates {
    public enum GameStateType {
        GameRunning, 
        GamePaused,
        MainMenu,
        GameWon,
        GameLost
    }

    /// <summary>
    /// Static class used as adaptor to convert strings to Statetypes and vise versa.
    /// </summary>
    public static class StateTransformer {
        public static GameStateType TransformStringToState(string state) {
            switch (state) {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                case "MAINMENU":
                    return GameStateType.MainMenu;
                case "GAME_LOST":
                    return GameStateType.GameLost;
                case "GAME_WON":
                    return GameStateType.GameWon;
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
                case GameStateType.GameLost:
                    return "GAME_LOST";
                case GameStateType.GameWon:
                    return "GAME_WON";
                default:
                    throw new ArgumentException("Not valid State");
            }
        }
    }
}