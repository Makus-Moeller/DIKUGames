using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using DIKUArcade.Events;
using Breakout;
using DIKUArcade.Input;

namespace Breakout.BreakoutStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons = new Text[2];
        private int activeMenuButton;

        private MainMenu() {
            Text newgame = (new Text("New Game", new Vec2F(0.4f, 0.3f), new Vec2F(0.3f, 0.3f)));
            Text quit = (new Text("Quit", (new Vec2F(0.4f, 0.2f)), new Vec2F(0.3f, 0.3f)));
            menuButtons[0] = newgame;
            menuButtons[1] = quit;
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)), 
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "BreakoutTitleScreen.png")));
        }

        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }
        
        public void RenderState() {
            backGroundImage.RenderEntity();
            switch (activeMenuButton) {
                case (0):
                    menuButtons[0].SetColor(120, 255, 0, 0);
                    menuButtons[1].SetColor(120, 0, 0, 255);
                    menuButtons[0].RenderText();
                    menuButtons[1].RenderText();
                    break;
                case (1):
                    menuButtons[0].SetColor(120, 0, 0, 255);
                    menuButtons[1].SetColor(120, 255, 0, 0);
                    menuButtons[0].RenderText();
                    menuButtons[1].RenderText();
                    break;
                default:
                    throw new ArgumentException("Ingen Buttons");
            }
        }

        public void UpdateGameLogic()
        {
           
        }
        public void GameLoop()
        {
            throw new System.NotImplementedException();
        }
         public void InitializeGameState()
        {
            throw new System.NotImplementedException();
        }

        public void ResetState()
        {
            throw new NotImplementedException();
        }

        public void UpdateState()
        {
            
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Up:
                    activeMenuButton = 0;
                    break;
                case KeyboardKey.Down:
                    activeMenuButton = 1;
                    break;
                case KeyboardKey.Enter:
                    if (action == KeyboardAction.KeyPress && activeMenuButton == 0) {
                        BreakoutBus.GetBus().RegisterEvent(
                             new GameEvent{EventType = GameEventType.GameStateEvent, 
                                Message = "CHANGE_STATE", StringArg1 = "GAME_RUNNING", StringArg2 = "MAINMENU"}
                        );
                    }
                    else if (action == KeyboardAction.KeyPress) {
                        BreakoutBus.GetBus().RegisterEvent(
                             new GameEvent{EventType = GameEventType.WindowEvent, Message = "CLOSE_WINDOW"}
                        );
                    }
                    break;
            }
        }
    }
}

