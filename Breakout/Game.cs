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

namespace Breakout {
    public class Game : DIKUGame, IGameEventProcessor  {
        private Player player;
        private GameEventBus eventBus; 

        public Game(WindowArgs winArgs) : base(winArgs)  {
            window.SetKeyEventHandler(KeyHandler);
            window.SetClearColor(System.Drawing.Color.Black);

            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.WindowEvent, GameEventType.TimedEvent});
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            eventBus.Subscribe(GameEventType.TimedEvent, this);

            //Instantiate player object etc. 
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")),
                (new RegularBuffState())); 
        }
        
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Num_1:
                        window.SetClearColor(System.Drawing.Color.Beige);
                        break;
                    case KeyboardKey.Num_2:
                        window.SetClearColor(System.Drawing.Color.Coral);
                        break;
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
        }

        public override void Update()
        {   
            player.Move();
            eventBus.ProcessEvents();
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