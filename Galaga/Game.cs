using DIKUArcade;
using DIKUArcade.Timers;
using System.Collections.Generic;
using DIKUArcade.EventBus;
using Galaga.GalagaStates;

namespace Galaga {
    public class Game : IGameEventProcessor<object> {
        private Window window;
        private GameTimer gameTimer;
        private StateMachine stateMachine;
        public Game() {    
            window = new Window("Galaga", 500, 500);            
            gameTimer = new GameTimer(30, 30);
            
            //Events
            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> { 
                GameEventType.InputEvent, 
                GameEventType.PlayerEvent, 
                GameEventType.GameStateEvent,
                GameEventType.WindowEvent,
                GameEventType.StatusEvent}); 
            //subscribing objects and eventtypes
            window.RegisterEventBus(GalagaBus.GetBus());
            GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);

            //Initializerer StateMachine
            stateMachine = new StateMachine();
        }
                 
        //Game håndterer nu kun window events Lige nu har vi kun QUIT_GAME
        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent) {
            if (type == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                    case "QUIT_GAME":
                        window.CloseWindow();
                        break;
                default:
                    break;
                }
            }
        }

        public void Run() {
            while(window.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    window.PollEvents();
                    GalagaBus.GetBus().ProcessEventsSequentially();
                    stateMachine.ActiveState.UpdateGameLogic();
                }
                
                if (gameTimer.ShouldRender()) {
                    window.Clear();
                    // render game entities here...
                    stateMachine.ActiveState.RenderState();
                    window.SwapBuffers();
                    
                }

                if (gameTimer.ShouldReset()) {
                    // this update happens once every second
                    window.Title = 
                        $"Galaga | (UPS,FPS): ({gameTimer.CapturedUpdates},{gameTimer.CapturedFrames})";
                }
            }
        }            
    }
}