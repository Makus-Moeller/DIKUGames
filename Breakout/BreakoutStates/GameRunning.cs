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

namespace Breakout.BreakoutStates
{
    /// <summary>
    /// GameRunning class. Where the breakout game is running.
    /// </summary>
    public class GameRunning : IGameState {
        private CollisionHandler collisionHandler;
        private Player player;
        public Rewards gamescore {get; private set;}
        private LevelLoader levelLoader;
        private EntityContainer<AtomBlock> AllBlocks;
        private EntityContainer<Ball> balls;
        private PlayerLives playerLives;
        
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
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")), 
                    new RegularBuffState()); 
            //Instantiates levelloader, balls and rewards    
            levelLoader = new LevelLoader(Path.Combine("Assets", "Levels"));
            balls = new EntityContainer<Ball>();
            balls.AddEntity(new Ball(new DynamicShape(new Vec2F(0.50f, 0.08f), new Vec2F(0.04f, 0.04f), 
                new Vec2F(0.01f, 0.02f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png"))));
            gamescore = new Rewards(new Vec2F(0.01f, 0.8f), new Vec2F(0.2f,0.2f));
            //Levelloader can set level
            AllBlocks = levelLoader.Nextlevel();
            collisionHandler = new CollisionHandler();
            playerLives = new PlayerLives(new Vec2F(0.03f, 0.01f), new Vec2F(0.2f, 0.2f), player);
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
            balls.RenderEntities();
            playerLives.RenderLives();
            levelLoader.timer.RenderTime();
        }

        /// <summary>
        /// Update dynamic states.
        /// </summary>
        public void UpdateState() {
            player.Move();
            balls.Iterate(ball => ball.MoveBall());
            BreakoutBus.GetBus().ProcessEvents();
            collisionHandler.HandleEntityCollisions(player, balls);
            playerLives.UpdateLives();
            balls.Iterate(ball => collisionHandler.HandleEntityCollisions(ball, AllBlocks));
            levelLoader.timer.UpdateTimeRemaining();

            if (AllBlocks.CountEntities() == 0) {
                AllBlocks = levelLoader.Nextlevel();
                balls.ClearContainer();
                balls.AddEntity(new Ball(new DynamicShape(new Vec2F(0.50f, 0.08f), new Vec2F(0.04f, 0.04f), 
                new Vec2F(0.01f, 0.02f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png"))));

            }
            if (balls.CountEntities() == 0) {
                player.DecrementLives();
                balls.AddEntity(new Ball(new DynamicShape(new Vec2F(0.50f, 0.08f), new Vec2F(0.04f, 0.04f), 
                new Vec2F(0.01f, 0.02f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png"))));
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