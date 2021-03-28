
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using DIKUArcade.EventBus;
using Galaga;    

namespace Galaga.GalagaStates {
    public class GamePaused : IGameState {
        private static GamePaused instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons = new Text[2];
        private int activeMenuButton;

        private GamePaused() {
            Text Countinue = (new Text("Continue", new Vec2F(0.4f, 0.4f), new Vec2F(0.3f, 0.3f)));
            Text MainMenu = (new Text("MainMenu", (new Vec2F(0.4f, 0.3f)), new Vec2F(0.3f, 0.3f)));
            menuButtons[0] = Countinue;
            menuButtons[1] = MainMenu;
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f), 
                new Vec2F(1.0f, 1.0f)), 
                new Image(Path.Combine("..", "Galaga", "Assets", "Images", "TitleImage.png")));
        }

        public static GamePaused GetInstance() {
            
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }

        public void GameLoop()
        {
            throw new NotImplementedException();
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
                        GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "CHANGE_STATE", "GAME_RUNNING", ""));
                    }
                    else {
                        GalagaBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "CHANGE_STATE", "MAINMENU", ""));
                    }
                    break;
            }
        }

        public void InitializeGameState()
        {
            throw new NotImplementedException();
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
    }
}