using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout;
using System.IO;

namespace Breakout.Levelloader {

    //Inderholder funktionalitet til at 
    public class LevelLoader {
        private IStringInterpreter stringInterpreter;
        private CharDefiners[] charDefiners;
        private IBlockCreator blockCreator;
        private List<string> filenames;
        private DirectoryReader directoryReader;
        //Hvis du vil sætte levelet skal du fortælle den :
        //Hvad det er baseret på, hvordan den skal læse og fortolke den
        //Og hvordan den skal genere entities ud fra det den læser.
        
        public LevelLoader() {
            directoryReader = new DirectoryReader();
            filenames = directoryReader.Readfiles(Path.Combine("Assets", "Levels"));
        }
        public EntityContainer<AtomBlock> SetLevel(string file, IStringInterpreter interpreter, IBlockCreator blockCreator) {
            stringInterpreter = interpreter;
            this.blockCreator = blockCreator;
            stringInterpreter.ReadFile(file);
            charDefiners = stringInterpreter.CreateCharDefiners();
            return blockCreator.CreateBlocks(charDefiners);
        }

        public EntityContainer<AtomBlock> Nextlevel() {
            EntityContainer<AtomBlock> levelBlocks;
            //When there are more levels. Load the next one
            if (filenames.Count > 0) {
                levelBlocks = SetLevel(Path.Combine("Assets", "Levels", filenames[0]), 
                    new StringTxtInterpreter(new StreamReaderClass()), new BlockCreator());
                filenames.Remove(filenames[0]);
                return levelBlocks; 
            }
            else
            //When levellist is empty change state to MainMenu
                levelBlocks = SetLevel(Path.Combine("Assets", "Levels", "empty.txt"), 
                    new StringTxtInterpreter(new StreamReaderClass()), new BlockCreator());
                BreakoutBus.GetBus().RegisterTimedEvent(
                    new GameEvent {EventType = GameEventType.TimedEvent, Message = "END_GAME"},
                    TimePeriod.NewSeconds(2.0));
                return levelBlocks;  
        }
    }
}