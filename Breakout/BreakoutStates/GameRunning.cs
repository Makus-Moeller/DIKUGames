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
        private PlayerLives playerLives;
        private PowerUpManager powerUpManger;
        private Wall wall;
        
        //private Timer timer;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
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
            balls.AddBall(new Vec2F(0.50f, 0.08f), new Vec2F(0.01f, 0.02f));
            gamescore = new Rewards(new Vec2F(0.01f, 0.8f), new Vec2F(0.2f,0.2f));
            //Levelloader can set level
            
            collisionHandler = new CollisionHandler();
            playerLives = new PlayerLives(new Vec2F(0.03f, 0.01f), new Vec2F(0.2f, 0.2f), player);
            //Playershots and image
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "BulletRed2.png"));
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
                    if (player.LaserAvailable) {
                        playerShots.AddEntity(new PlayerShot(
                        new Vec2F(player.GetPosition().X + (player.ExtentX / 2), 
                        player.GetPosition().Y), playerShotImage));
                    }
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

         private void IterateShots() {
            playerShots.Iterate(shot => {
                //move the shots shape
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    //Delete shot
                    shot.DeleteEntity();
                }
            });
        }

        /// <summary>
        /// Render objects on window.
        /// </summary>
        public void RenderState() {
            gamescore.RenderScore();
            player.Render();
            AllBlocks.RenderEntities();
            balls.allBalls.RenderEntities();
            playerLives.RenderLives();
            levelLoader.timer.RenderTime();
            powerUpManger.RenderPowerUps();
            wall.RenderWall();
            playerShots.RenderEntities();
        }

        /// <summary>
        /// Update dynamic states.
        /// </summary>
        public void UpdateState() {
            player.Move();
            balls.allBalls.Iterate(ball => ball.MoveBall());
            BreakoutBus.GetBus().ProcessEvents();
            collisionHandler.HandleEntityCollisions(player, balls.allBalls);
            playerLives.UpdateLives();
            powerUpManger.Update();
            collisionHandler.HandleEntityCollisions(player, powerUpManger.CurrentPowerUps);
            balls.allBalls.Iterate(ball => collisionHandler.HandleEntityCollisions(ball, AllBlocks));
            levelLoader.timer.UpdateTimeRemaining();
            
            if (wall.IsActive) {
                collisionHandler.HandleEntityCollisions(wall, balls.allBalls);    
            }
            playerShots.Iterate(playerShot => collisionHandler.HandleEntityCollisions(playerShot, AllBlocks));
            IterateShots();

            if (AllBlocks.CountEntities() == 0) {
                AllBlocks = levelLoader.Nextlevel();
                balls.allBalls.ClearContainer();
                balls.AddBall(new Vec2F(0.50f, 0.08f), new Vec2F(0.01f, 0.02f));

            }
            if (balls.allBalls.CountEntities() == 0) {
                player.DecrementLives();
                balls.AddBall(new Vec2F(0.50f, 0.08f), new Vec2F(0.01f, 0.02f));
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