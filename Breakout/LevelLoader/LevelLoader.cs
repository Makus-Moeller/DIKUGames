using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout;
using System.IO;

namespace Breakout.Levelloader {

    /// <summary>
    ///Handles the changing of levels
    /// </summary>
    public class LevelLoader {
        private IStringInterpreter stringInterpreter;
        private CharDefiners[] charDefiners;
        private IBlockCreator blockCreator;
        private List<string> filenames;
        private DirectoryReader directoryReader;
        private string path;
        //Hvis du vil sætte levelet skal du fortælle den :
        //Hvad det er baseret på, hvordan den skal læse og fortolke den
        //Og hvordan den skal genere entities ud fra det den læser.
        
        public LevelLoader(string path) {
            directoryReader = new DirectoryReader();
            filenames = directoryReader.Readfiles(path);
            this.path = path;
        }

        /// <summary>
        /// Allows you to change level even though its a different type of file
        /// or the chardefiners need to be interpreted different
        /// </summary>
        /// <param name="file">File to be read</param>
        /// <param name="interpreter">interpreter to chardefiners</param>
        /// <param name="blockCreator">interpreter of chardefiners</param>
        public EntityContainer<AtomBlock> SetLevel(string file, IStringInterpreter interpreter, 
                IBlockCreator blockCreator) {
            stringInterpreter = interpreter;
            this.blockCreator = blockCreator;
            stringInterpreter.ReadFile(file);
            charDefiners = stringInterpreter.CreateCharDefiners();
            return blockCreator.CreateBlocks(charDefiners);
        }
        /// <summary>
        ///changes to nex level
        /// </summary>
        public EntityContainer<AtomBlock> Nextlevel() {
            EntityContainer<AtomBlock> levelBlocks;
            //When there are more levels. Load the next one
            if (filenames.Count > 0) {
                levelBlocks = SetLevel(Path.Combine(path, filenames[0]), 
                    new StringTxtInterpreter(new StreamReaderClass()), new BlockCreator());
                filenames.Remove(filenames[0]);
                return levelBlocks; 
            }
            else
            //When levellist is empty change state to GameWon
                levelBlocks = SetLevel(Path.Combine(path, "empty.txt"), 
                    new StringTxtInterpreter(new StreamReaderClass()), new BlockCreator());
                BreakoutBus.GetBus().RegisterTimedEvent(
                    new GameEvent {EventType = GameEventType.TimedEvent, Message = "END_GAME"},
                    TimePeriod.NewSeconds(2.0));
                return levelBlocks;  
        }
    }
}