using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Entities;
namespace Breakout.Levelloader {


    //Inderholder funktionalitet til at 
    public class LevelLoader {
        private IStringInterpreter stringInterpreter;
        private CharDefiners[] charDefiners;
        private IBlockCreator blockCreator;
        //Hvis du vil sætte levelet skal du fortælle den :
        //Hvad det er baseret på, hvordan den skal læse og fortolke den
        //Og hvordan den skal genere entities ud fra det den læser.
        public EntityContainer<AtomBlock> SetLevel(string file, IStringInterpreter interpreter, IBlockCreator blockCreator) {
            stringInterpreter = interpreter;
            this.blockCreator = blockCreator;
            stringInterpreter.ReadFile(file);
            charDefiners = stringInterpreter.CreateCharDefiners();
            return blockCreator.CreateBlocks(charDefiners);
        }
    }
}