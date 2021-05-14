using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Events;
using System;
namespace Breakout.Blocks {

    public class HardenedBlock : AtomBlock {
        private string OriginalPath {get;}
        
        public HardenedBlock(Shape shape, IBaseImage image, string path) : base(shape, image) {
            isHardened = true;
            hitpoints += 4; 
            value += 1;
            OriginalPath = path;
        }
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