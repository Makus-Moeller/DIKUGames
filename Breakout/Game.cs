using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;

namespace Breakout {
    public class Game : DIKUGame {
        public Game(WindowArgs winArgs) : base(winArgs)  {
            window.SetKeyEventHandler(keyHandler);
            window.SetClearColor(System.Drawing.Color.BlueViolet);
        }
        
        private void keyHandler(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Num_1:
                        window.SetClearColor(System.Drawing.Color.Beige);
                        break;
                    case KeyboardKey.Num_2:
                        window.SetClearColor(System.Drawing.Color.Coral);
                        break;
                    default:
                        break;
                }
            }
            else if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Escape:
                        window.CloseWindow();
                        break;
                    default:
                        break;
                }
            }
            

        }
        public override void Render()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}