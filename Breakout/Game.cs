using System;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Timers;
using Breakout.Players;
using Breakout.BreakoutStates;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using Breakout.Levelloader;
using Breakout.Blocks;

namespace Breakout {
    public class Game : DIKUGame, IGameEventProcessor  {
        private Player player;
        private Rewards gamescore;
        private GameEventBus eventBus; 
        private LevelLoader levelLoader;
        private EntityContainer<Entity> AllEntities;
        private EntityContainer<AtomBlock> AllBlocks;
        private Ball ball;

        private StateMachine stateMachine;
        public Game(WindowArgs winArgs) : base(winArgs)  {
            window.SetKeyEventHandler(KeyHandler);
            window.SetClearColor(System.Drawing.Color.Black);

            //In case we want to use the eventbus later to implement gamestates
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
                GameEventType.WindowEvent, GameEventType.TimedEvent, GameEventType.StatusEvent});
            BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, this);


            //Statemachine

            stateMachine = new StateMachine();


            ball = new Ball(new DynamicShape(new Vec2F(0.30f, 0.08f), new Vec2F(0.04f, 0.04f), new Vec2F(0.005f, 0.006f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            gamescore = new Rewards(new Vec2F(0.01f, 0.9f), new Vec2F(0.1f,0.1f));
        }
        
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Left:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {    
                            EventType = GameEventType.InputEvent, Message = "KEY_LEFT"});
                        //player.SetMoveLeft(true);
                        break;
                    case KeyboardKey.Right:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {    
                            EventType = GameEventType.InputEvent, Message = "KEY_RIGHT"});
                        //player.SetMoveRight(true);
                        break;
                    case KeyboardKey.Up:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent { 
                            EventType = GameEventType.InputEvent, Message = "KEY_UP"});
                            break;
                    case KeyboardKey.Down:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent { 
                            EventType = GameEventType.InputEvent, Message = "KEY_DOWN"});
                            break;
                    default:
                        break;
                }
            }
            else if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Escape:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {    
                            EventType = GameEventType.InputEvent, Message = "ESCAPE"});
                        //window.CloseWindow();
                        break;
                    //Used to count the time spent on a specific level 
                    case KeyboardKey.Space:
                        BreakoutBus.GetBus().RegisterTimedEvent(
                            new GameEvent {EventType = GameEventType.TimedEvent, Message = "HELLO"},
                                TimePeriod.NewSeconds(2.0));
                        break;
                    case KeyboardKey.Left:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {    
                            EventType = GameEventType.InputEvent, Message = "KEY_LEFT_RELEASED"});
                        //player.SetMoveLeft(false);
                        break;
                    case KeyboardKey.Right:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {    
                            EventType = GameEventType.InputEvent, Message = "KEY_RIGHT_RELEASED"});
                        //player.SetMoveRight(false);
                        break;
                    case KeyboardKey.Enter:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {    
                            EventType = GameEventType.InputEvent, Message = "ENTER"});
                        //player.SetMoveRight(false);
                        break;
                    default:
                        break;
                }
            } 
        }
        public override void Render() {
            gamescore.RenderScore();
            player.Render();
            AllBlocks.RenderEntities();
            ball.RenderBall();
        }

        public override void Update() {   
            player.Move();
            BreakoutBus.GetBus().ProcessEvents();
            ball.UpdateBall(AllBlocks, player);
            
        }

        public void ProcessEvent(GameEvent gameEvent)
        {
            if (gameEvent.Message == "CLOSE_WINDOW") {
                Console.WriteLine("Received close window event");
                window.CloseWindow();
            }
            else if (gameEvent.Message == "HELLO") {
                Console.WriteLine("Received HELLO message");
            }
        }
    }
}