using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using DIKUArcade.EventBus;
using GalagaStates;
namespace GalagaStates {
    public class GameRunning : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons = new Text[2];
        private int activeMenuButton;
        private int maxMenuButtons;

        public void GameLoop()
        {
            throw new NotImplementedException();
        }

        public void HandleKeyEvent(string keyValue, string keyAction)
        {
            throw new NotImplementedException();
        }

        public void InitializeGameState()
        {
            throw new NotImplementedException();
        }

        public void RenderState()
        {
            throw new NotImplementedException();
        }

        public void UpdateGameLogic()
        {
            throw new NotImplementedException();
        }
    }
}