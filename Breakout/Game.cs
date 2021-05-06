using System;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Timers;
using Breakout.Players;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using Breakout.Levelloader;
using Breakout.Blocks;
namespace Breakout {
    public class Game : DIKUGame, IGameEventProcessor  {
        private Player player;
        private GameEventBus eventBus; 
        private LevelLoader levelLoader;
        private EntityContainer<AtomBlock> AllBlocks;
        private Ball ball;

        public Game(WindowArgs winArgs) : base(winArgs)  {
            window.SetKeyEventHandler(KeyHandler);
            window.SetClearColor(System.Drawing.Color.Black);

            //In case we want to use the eventbus later to implement gamestates
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> {
                GameEventType.WindowEvent, GameEventType.TimedEvent});
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            eventBus.Subscribe(GameEventType.TimedEvent, this);

            //Instantiate player object etc. 
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")), 
                    new RegularBuffState()); 
            //Instantiates levelloader    
            levelLoader = new LevelLoader();
            //Levelloader can set level
            AllBlocks = levelLoader.SetLevel(Path.Combine("Assets", "Levels", "level2.txt"), 
                new StringTxtInterpreter(new StreamReaderClass()), new BlockCreator());

            ball = new Ball(new DynamicShape(new Vec2F(0.30f, 0.08f), new Vec2F(0.04f, 0.04f), new Vec2F(0.0f, 0.004f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));
        }
        
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Left:
                        player.SetMoveLeft(true);
                        break;
                    case KeyboardKey.Right:
                        player.SetMoveRight(true);
                        break;
                    default:
                        break;
                }
            }
            else if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Escape:
                        Console.WriteLine("Sending close window event");
                        eventBus.RegisterEvent(new GameEvent {    
                            EventType = GameEventType.WindowEvent, Message = "CLOSE_WINDOW"});
                        //window.CloseWindow();
                        break;
                    //Used to count the time spent on a specific level 
                    case KeyboardKey.Space:
                        eventBus.RegisterTimedEvent(
                            new GameEvent {EventType = GameEventType.TimedEvent, Message = "HELLO"},
                                TimePeriod.NewSeconds(2.0));
                        break;
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
        public override void Render()
        {
            player.Render();
            foreach (AtomBlock block in AllBlocks)
            {
                block.RenderEntity();
            }
            ball.RenderEntity();
        }

        public override void Update() {   
            player.Move();
            eventBus.ProcessEvents();
            ball.MoveBall();
            AllBlocks.Iterate(block => ball.HandleCollision(block));
            ball.HandleCollision(player);
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