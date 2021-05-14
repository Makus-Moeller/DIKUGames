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
using Breakout.Blocks;

namespace Breakout.BreakoutStates
{
    /// <summary>
    /// GameRunning class. Where the breakout game is running.
    /// </summary>
    public class GameRunning : IGameState {
        private CollisionHandler collisionHandler;
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

        /// <summary>
        /// Initializes a new game. 
        /// </summary>
        public void InitializeGameState() {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")), 
                    new RegularBuffState()); 
            //Instantiates levelloader, ball and rewards    
            levelLoader = new LevelLoader(Path.Combine("Assets", "Levels"));
            ball = new Ball(new DynamicShape(new Vec2F(0.50f, 0.08f), new Vec2F(0.04f, 0.04f), 
                new Vec2F(0.004f, 0.007f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));
            gamescore = new Rewards(new Vec2F(0.01f, 0.8f), new Vec2F(0.2f,0.2f));
            //Levelloader can set level
            AllBlocks = levelLoader.Nextlevel();
            collisionHandler = new CollisionHandler(player, ball, AllBlocks);
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
            ball.RenderBall();
        }

        /// <summary>
        /// Update dynamic states.
        /// </summary>
        public void UpdateState() {
            player.Move();
            ball.MoveBall();
            BreakoutBus.GetBus().ProcessEvents();
            collisionHandler.HandleCollisions();
            if (AllBlocks.CountEntities() == 0) {
                AllBlocks = levelLoader.Nextlevel();
                collisionHandler.InitializeCollisionHandler(AllBlocks);
            }
        }

        public void ResetState()
        {
            throw new NotImplementedException();
        }
    }
}