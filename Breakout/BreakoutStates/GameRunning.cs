using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using DIKUArcade.Events;
using Breakout;
using Breakout.Players;
using Breakout.Levelloader;
using System.Collections.Generic;
using DIKUArcade.Physics;
using DIKUArcade.Input;

namespace Breakout.BreakoutStates{
    public class GameRunning : IGameState {
        private Player player;
        private static GameRunning instance = null;
        //private GameEventBus eventBus; 
        private LevelLoader levelLoader;
        public GameRunning() {
            InitializeGameState();
        }

        public static GameRunning GetInstance() {
            
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
        
        public void GameLoop()
        {
            throw new NotImplementedException();
        } 
        public void InitializeGameState() {
            //Sætter Enemy count til 0 for at sikre den ikke tæller enemys fra tidligere spil
            //når vi siger new game efter at have initializeret GameRunning første gang.
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")), 
                    new RegularBuffState()); 
            //Instantiates levelloader    
            levelLoader = new LevelLoader();
            //Levelloader can set level
            levelLoader.SetLevel(Path.Combine("Assets", "Levels", "level2.txt"), 
                new StringTxtInterpreter(new StreamReaderClass()), new BlockCreator());
        }

        public void RenderState() {
            player.Render();
            levelLoader.RenderBlocks();
        }


        public void ResetState()
        {
            throw new NotImplementedException();
        }

        public void UpdateState()
        {
            player.Move();
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
                default:
                    break;
            }
        }
                

        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    player.SetMoveLeft(true);
                    break;
                case KeyboardKey.Right:
                    player.SetMoveRight(true);
                    break;
                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{EventType = GameEventType.GameStateEvent, Message = "CHANGE_STATE", 
                            StringArg1 = "GAME_PAUSED", StringArg2 = "GAME_RUNNING"});
                    break;
                default:
                    break;
            }
        }

        public void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    player.SetMoveLeft(false);
                    break;
                case KeyboardKey.Right:
                    player.SetMoveRight(false);
                    break;
                default:
                    break;
            }    
        }
    }
}