using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using System.Collections.Generic;
using DIKUArcade.Events;
using Breakout.Players;
using Breakout;
using Breakout.Levelloader;
using DIKUArcade.Input;
using Breakout.Blocks;

namespace Breakout.BreakoutStates
{
    public class GameRunning : IGameState {
        private Player player;
        private Rewards gamescore;
        private LevelLoader levelLoader;
        private EntityContainer<AtomBlock> AllBlocks;
        private Ball ball;
        private static GameRunning instance = null;
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
            
            ball = new Ball(new DynamicShape(new Vec2F(0.50f, 0.08f), new Vec2F(0.04f, 0.04f), new Vec2F(0.002f, 0.005f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            gamescore = new Rewards(new Vec2F(0.01f, 0.8f), new Vec2F(0.2f,0.2f));

            //Levelloader can set level
            AllBlocks = levelLoader.Nextlevel();
        }


        private void KeyPress(KeyboardKey key) {
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

        private void KeyRelease(KeyboardKey key) {
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
        public void RenderState() {
            gamescore.RenderScore();
            player.Render();
            AllBlocks.RenderEntities();
            ball.RenderBall();
        }


        public void ResetState()
        {
            throw new NotImplementedException();
        }

        public void UpdateState()
        {
            player.Move();
            BreakoutBus.GetBus().ProcessEvents();
            ball.UpdateBall(AllBlocks, player);
            if (AllBlocks.CountEntities() == 0) {
                AllBlocks = levelLoader.Nextlevel();
            }
        }
    }
}