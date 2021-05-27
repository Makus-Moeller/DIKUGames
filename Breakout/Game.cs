using System;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Timers;
using Breakout.BreakoutStates;

namespace Breakout {

    /// <summary>
    /// Main class of the game. Handles windowevents and initializes gamebus
    /// also updates game based on state
    /// </summary>
    public class Game : DIKUGame, IGameEventProcessor  {
        
        private StateMachine stateMachine;
        public Game(WindowArgs winArgs) : base(winArgs)  {
            window.SetKeyEventHandler(KeyHandler);
            window.SetClearColor(System.Drawing.Color.Black);

            //Intializing eventBus
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
                GameEventType.WindowEvent, GameEventType.TimedEvent, GameEventType.StatusEvent,
                    GameEventType.InputEvent, GameEventType.GameStateEvent, GameEventType.ControlEvent});
            
            BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
            
            //Not used yet
            BreakoutBus.GetBus().Subscribe(GameEventType.TimedEvent, this);

            //Statemachine

            stateMachine = new StateMachine();
        }
        
        /// <summary>
        /// Creates an event depending on input
        /// </summary>
        /// <param name="action">was a key pressed or released</param>
        /// <param name="key">what key was involved</param>
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Left:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {    
                            EventType = GameEventType.InputEvent, Message = "KEY_LEFT"});
                        break;
                    case KeyboardKey.Right:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {    
                            EventType = GameEventType.InputEvent, Message = "KEY_RIGHT"});
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
                        break;
                    //Will be used to count the time spent on a specific level 
                    case KeyboardKey.Space:
                        BreakoutBus.GetBus().RegisterTimedEvent(
                            new GameEvent {EventType = GameEventType.TimedEvent, Message = "HELLO"},
                                TimePeriod.NewSeconds(2.0));
                        break;
                    case KeyboardKey.Left:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {    
                            EventType = GameEventType.InputEvent, Message = "KEY_LEFT_RELEASED"});
                        break;
                    case KeyboardKey.Right:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {    
                            EventType = GameEventType.InputEvent, Message = "KEY_RIGHT_RELEASED"});
                        break;
                    case KeyboardKey.Enter:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {    
                            EventType = GameEventType.InputEvent, Message = "ENTER"});
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

        /// <summary>
        /// Handles window events and delegates to keyhandler
        /// </summary>
        /// <param name="gameEvent">The type of event</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent) {
                window.CloseWindow();
            }
            else if (gameEvent.EventType == GameEventType.TimedEvent) {
                if (gameEvent.Message == "END_GAME") {
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE", StringArg1 = "GAME_WON", StringArg2 = "GAME_RUNNING"});    
                }
            }
        }
    }
}