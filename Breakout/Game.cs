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
        
        private StateMachine stateMachine;
        public Game(WindowArgs winArgs) : base(winArgs)  {
            window.SetKeyEventHandler(KeyHandler);
            window.SetClearColor(System.Drawing.Color.Black);

            //Intializing eventBus
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
                GameEventType.WindowEvent, GameEventType.TimedEvent, GameEventType.StatusEvent, GameEventType.InputEvent, GameEventType.GameStateEvent});
            
            BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, this);

            //Statemachine

            stateMachine = new StateMachine();
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
            stateMachine.ActiveState.RenderState();
        }

        public override void Update() {   
            BreakoutBus.GetBus().ProcessEvents();
            stateMachine.ActiveState.UpdateState();
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "CLOSE_WINDOW") {
                window.CloseWindow();
            }
        }
    }
}