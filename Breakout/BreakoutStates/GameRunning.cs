using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using DIKUArcade.Events;
using Breakout.Players;
using Breakout.Levelloader;
using DIKUArcade.Input;
using DIKUArcade.Timers;
using Breakout.Blocks;
using Breakout.PowerUpSpace;
using Breakout.Balls;
using DIKUArcade.Utilities;

namespace Breakout.BreakoutStates {
    /// <summary>
    /// GameRunning class. Where the breakout game is running.
    /// </summary>
    public class GameRunning : IGameState {
        private CollisionHandler collisionHandler;
        private Player player;
        public Rewards gamescore {get; private set;}
        private LevelLoader levelLoader;
        private EntityContainer<AtomBlock> AllBlocks;
        private BallManager balls;
        private PowerUpManager powerUpManger;
        private Wall wall;
        
        //private Timer timer;
        private static GameRunning instance = null;
        public GameRunning() {
            new StaticTimer();
            InitializeGameState();
        }

        public static GameRunning GetInstance() {
            
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        /// <summary>
        /// Initializes a new game. 
        /// </summary>
        public void InitializeGameState() {
            powerUpManger = new PowerUpManager();
            levelLoader = new LevelLoader(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels"));
            AllBlocks = levelLoader.Nextlevel();
            wall = new Wall(new StationaryShape(new Vec2F(0.0f, 0.01f), new Vec2F(1.0f, 0.05f)),
                new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "wall.png")));

            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "player.png")), 
                    new RegularBuffState()); 
            //Instantiates levelloader, balls and rewards    

            balls = new BallManager();
            balls.AddBall(new Vec2F(0.50f, 0.08f), new Vec2F(0.005f, 0.015f));
            gamescore = new Rewards(new Vec2F(0.01f, 0.8f), new Vec2F(0.2f,0.2f));
            //Levelloader can set level
            
            collisionHandler = new CollisionHandler();

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
                case KeyboardKey.Space:
                    player.Shoot(player.GetPosition(), player.Shape.Extent);
                    break;
                default:
                    break;
            }    
        }

        /// <summary>
        /// Handle keyevents sent from statemachine.
        /// </summary>
        /// <param name="action">Whether its a keypress or keyrelease</param>
        /// <param name="key">The key</param>
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

        /// <summary>
        /// Render objects on window.
        /// </summary>
        public void RenderState() {
            gamescore.RenderScore();
            player.Render();
            AllBlocks.RenderEntities();
            balls.allBalls.RenderEntities();
            levelLoader.timer.RenderTime();
            powerUpManger.RenderPowerUps();
            wall.RenderWall();
        }

        /// <summary>
        /// Update dynamic states.
        /// </summary>
        public void UpdateState() {
            BreakoutBus.GetBus().ProcessEvents();
            player.Move();
            balls.allBalls.Iterate(ball => {
                ball.MoveBall();
                collisionHandler.HandleEntityCollisions(ball, AllBlocks);
            });
            
            if (wall.IsActive) {
                collisionHandler.HandleEntityCollisions(wall, balls.allBalls);    
            }
            
            collisionHandler.HandleEntityCollisions(player, balls.allBalls);
            collisionHandler.HandleEntityCollisions(player, powerUpManger.CurrentPowerUps);
            
            player.Weapon.AllShots.Iterate(playerShot => {
                collisionHandler.HandleEntityCollisions(playerShot, AllBlocks);
                });

            powerUpManger.Update();
            levelLoader.timer.UpdateTimeRemaining();
            if (AllBlocks.CountEntities() == 0) {
                AllBlocks = levelLoader.Nextlevel();
                balls.allBalls.ClearContainer();
                balls.AddBall(new Vec2F(0.50f, 0.08f), new Vec2F(0.005f, 0.015f));

            }
            if (balls.allBalls.CountEntities() == 0) {
                player.DecrementLives();
                balls.AddBall(new Vec2F(0.50f, 0.08f), new Vec2F(0.005f, 0.015f));
            }
            if (player.IsDead || levelLoader.timer.IsTimesUp()) {
                BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent{EventType = GameEventType.GameStateEvent, 
                            Message = "CHANGE_STATE", StringArg1 = "GAME_LOST", StringArg2 = "GAME_RUNNING"});
            } 
        }

        public void ResetState()
        {
            throw new NotImplementedException();
        }
    }
}