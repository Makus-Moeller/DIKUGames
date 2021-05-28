
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using DIKUArcade.Events;
using Breakout;
using DIKUArcade.Input;
using DIKUArcade.Utilities;

namespace Breakout.BreakoutStates {

    /// <summary>
    /// GamePaused class. Player can return to game or go to main menu.
    /// </summary>
    public class GamePaused : IGameState {
        private static GamePaused instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons = new Text[2];
        private int activeMenuButton;

        private GamePaused() {
            Text Countinue = (new Text("Continue", new Vec2F(0.4f, 0.3f), new Vec2F(0.3f, 0.3f)));
            Text MainMenu = (new Text("MainMenu", (new Vec2F(0.4f, 0.2f)), new Vec2F(0.3f, 0.3f)));
            menuButtons[0] = Countinue;
            menuButtons[1] = MainMenu;
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f), 
                new Vec2F(1.0f, 1.0f)), 
                new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "BreakoutTitleScreen.png")));
        }

        public static GamePaused GetInstance() {
            
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }

        /// <summary>
        /// Handle keyevents sent from statemachine.
        /// </summary>
        /// <param name="action">Whether its a keypress or keyrelease</param>
        /// <param name="key">The key</param>

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Up:
                    activeMenuButton = 0;
                    break;
                case KeyboardKey.Down:
                    activeMenuButton = 1;
                    break;
                case KeyboardKey.Enter:
                    if (activeMenuButton == 0) {
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent{EventType = GameEventType.GameStateEvent, 
                                Message = "CHANGE_STATE", StringArg1 = "GAME_RUNNING", StringArg2 = "GAME_PAUSED"});   
                    }
                    else {
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent{EventType = GameEventType.GameStateEvent, 
                                Message = "CHANGE_STATE", StringArg1 = "MAINMENU", StringArg2 = "GAME_PAUSED"});
                    }
                    break;
            }
        }

        /// <summary>
        /// User interface for main menu. Render buttons and pause image.
        /// </summary>
        public void RenderState() {
                    backGroundImage.RenderEntity();
                    switch (activeMenuButton) {
                        case (0):
                            menuButtons[0].SetColor(255, 255, 0, 0);
                            menuButtons[1].SetColor(255, 0, 0, 255);
                            menuButtons[0].RenderText();
                            menuButtons[1].RenderText();
                            break;
                        case (1):
                            menuButtons[0].SetColor(255, 0, 0, 255);
                            menuButtons[1].SetColor(255, 255, 0, 0);
                            menuButtons[0].RenderText();
                            menuButtons[1].RenderText();
                            break;
                        default:
                            throw new ArgumentException("Ingen Buttons");
                    }
                }

        public void ResetState()
        {
            throw new NotImplementedException();
        }
        public void UpdateState()
        {
        }
    }
}