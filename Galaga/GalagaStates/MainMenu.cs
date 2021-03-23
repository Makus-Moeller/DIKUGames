using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using DIKUArcade.EventBus;
using GalagaStates;
namespace GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons = new Text[2];
        private int activeMenuButton;
        private int maxMenuButtons;

        private MainMenu()
        {
            Text newgame = (new Text("New Game", new Vec2F(0.4f, 0.4f), new Vec2F(0.2f, 0.2f)));
            Text quit = (new Text("Quit", (new Vec2F(0.4f, 0.2f)), new Vec2F(0.2f, 0.2f)));
            menuButtons[0] = newgame;
            menuButtons[1] = quit;
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)), new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
        }

        public static MainMenu GetInstance() {
            
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }
        public void GameLoop()
        {
            throw new System.NotImplementedException();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyValue) {
                case "KEY_UP":
                    activeMenuButton = 0;
                    break;
                case "KEY_DOWN":
                    activeMenuButton = 1;
                    break;
                case "KEY_ENTER":
                    if (activeMenuButton == 0) {
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "CHANGE_STATE", "GAME_RUNNING", "");
                    }
                    else {
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.InputEvent, this, "KEY_ESCAPE", keyAction, "");
                    }
                    break;
            }
        }

        public void InitializeGameState()
        {
            throw new System.NotImplementedException();
        }

        public void RenderState()
        {
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
            backGroundImage.RenderEntity();
        }

        public void UpdateGameLogic()
        {
            throw new System.NotImplementedException();
        }
    }
}

