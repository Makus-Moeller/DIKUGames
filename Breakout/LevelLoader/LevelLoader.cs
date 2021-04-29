using System.IO;
using System;
using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Math;
namespace Breakout.Levelloader {
    public class LevelLoader {

        private StringInterpreter stringInterpreter;

        public LevelLoader()
        {
        }
    }
    public interface IBlockCreator {
        IBlocks createblock(string pathToImage, Vec2F position);
    }
    public class BlockCreator {
        private StringInterpreter stringInterpreter;

        public BlockCreator(string txtFile) {
            stringInterpreter = new StringInterpreter(txtFile);
        }
        /*
        public IBlocks createblock(string pathToImage, Vec2F position) {
            IBlocks thisblock = new();
        }
        */
    }



    ///Bruger streamreader til at lave 3 datastrukturer og udvælger det vigtige
    public class StringInterpreter {
        private StreamReaderClass reader;
        private string[] legendData;
        private string[] mapData;
        private string[] metaData;
        public CharDefiners[] arrayOfCharDefiners {get; private set;}
        public StringInterpreter(string txtFile) {
            reader = new StreamReaderClass();
            mapData = reader.txtToArray(txtFile, "Map:", "Map/");
            legendData = reader.txtToArray(txtFile, "Legend:", "Legend/");
            metaData = reader.txtToArray(txtFile, "Meta:", "Meta/");
        }
        public void CreateCharDefiners() {
            int amountOfChars = legendData.Length;
            char powerup = ' ';
            char harden = ' ';
            char unbreakable = ' ';
            arrayOfCharDefiners = new CharDefiners[amountOfChars];
            for (int i = 0; i < metaData.Length; i++) {
                if (metaData[i][0] == 'P') {
                    powerup = metaData[i][9];
                }
                if (metaData[i][0] == 'H') {
                    harden = metaData[i][10];
                }
                if (metaData[i][0] == 'U') {
                    unbreakable = metaData[i][13];
                }
            }
            for (int i = 0; i < amountOfChars; i++) {
                arrayOfCharDefiners[i] = new CharDefiners();
                arrayOfCharDefiners[i].character = legendData[i][0];
                arrayOfCharDefiners[i].imagePath = legendData[i].Remove(0, 2);
                if (arrayOfCharDefiners[i].character == powerup) {
                    arrayOfCharDefiners[i].powerUp = true;
                } 
                if (arrayOfCharDefiners[i].character == harden) {
                    arrayOfCharDefiners[i].hardened = true;
                } 
                if (arrayOfCharDefiners[i].character == unbreakable) {
                    arrayOfCharDefiners[i].unbreakable = true;
                } 
                
            } 
        }
    }



    ///Læste en txtfil og returnere det brugbare i et array af strings        
    public class StreamReaderClass {
        private int countNumberOfValidLines(string txtFile, string startingpoint, string breakpoint) {
            int numberOfLines = 0;
            string line;
            System.IO.StreamReader file =
                new System.IO.StreamReader(txtFile);
            while ((line = file.ReadLine()) != startingpoint) {

            }
            while((line = file.ReadLine()) != breakpoint)  
            {    
                numberOfLines++;
            }
            return numberOfLines;
        }
        
        public string[] txtToArray(string txtFile, string startingpoint, string breakpoint) {
            string[] stringArray = new string[countNumberOfValidLines(txtFile, startingpoint, breakpoint)];
            string line;
            int lineNumber = 0;
            System.IO.StreamReader file =
                new System.IO.StreamReader(txtFile);
            while (startingpoint != (line = file.ReadLine())) {
            }
            while((line = file.ReadLine()) != breakpoint)  
            {  
                if (line == startingpoint) {}
                else {
                stringArray[lineNumber] = line;    
                lineNumber++;
                }
            }
            return stringArray;
        }
    }
}