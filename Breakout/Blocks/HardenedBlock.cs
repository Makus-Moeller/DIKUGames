using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Events;
using System;

namespace Breakout.Blocks {
    /// <summary>
    /// A special hardened block
    /// </summary>
    public class HardenedBlock : AtomBlock {
        private string OriginalPath {get;}
        
        public HardenedBlock(Shape shape, IBaseImage image, string path) : base(shape, image) {
            isHardened = true;
            hitpoints += 4; 
            value += 1;
            OriginalPath = path;
        }

        /// <summary>
        /// Hit block by certain amount. When histpoints are under Tresshold of 50%
        /// Image is changed.
        /// </summary>
        /// <param name="decrementValue">Amount a block is hit by</param>
        public override void HitBlock(int decrementValue) {
            Console.WriteLine(OriginalPath);
            Console.WriteLine((OriginalPath.Remove(OriginalPath.Length-4, 4) + "-damaged.png"));
            if (hitpoints - decrementValue < hitpoints / 2 + 1) {
                Image = new Image(Path.Combine("..", "Breakout", 
                            "Assets", "Images", (OriginalPath.Remove(OriginalPath.Length-4, 4) + "-damaged.png")));
            } 
            if ((hitpoints -= decrementValue) < 1) {
                this.DeleteEntity();
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{EventType = GameEventType.StatusEvent,
                    Message = "INCREASE_SCORE",
                    StringArg2 = value.ToString(),
                    });
            }
        }
    }
}