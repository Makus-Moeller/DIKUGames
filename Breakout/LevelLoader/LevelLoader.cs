using System.IO;
using System;
using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Math;
namespace Breakout.Levelloader {
    public class Levelloader {




    }
    public interface IBlockCreator {
        IBlocks createblock(string pathToImage, Vec2F position);
    }
    public class BlockCreator : IBlockCreator {
        private StringInterpreter stringInterpreter;

        public BlockCreator(string txtFile) {
            stringInterpreter = new StringInterpreter(txtFile);
        }

        public IBlocks createBlock(string pathToImage, Vec2F position) {
            IBlocks thisblock = new();
        }
    }



    ///Bruger streamreader til at lave 3 datastrukturer og udvælger det vigtige
    public class StringInterpreter {
        private StreamReaderClass reader;
        private string[] legendData;
        private string[] mapData;
        private string[] metaData;
        private List<IBlocks[]> listOfBlocks;
        private IBlockCreator blockCreator;
        public StringInterpreter(string txtFile) {
            reader = new StreamReaderClass();
            mapData = reader.txtToArray(txtFile, "Map:", "Map/");
            legendData = reader.txtToArray(txtFile, "Legend:", "Legend/");
            metaData = reader.txtToArray(txtFile, "Meta:", "Meta/");
        }
        private void defineCharacthers() {
            
        }
        private IBlocks[] lineToBlockCreator (string lineToLoad) {
            IBlocks[] lineOfBlocks = new IBlocks[12];
            




            for (int i = 0; i < 13; i++)
            {
                lineOfBlocks[i] = blockCreator.createblock();
            }
        }
        public List<IBlocks[]> MakeLevel(){
            foreach (string line in mapData)
            {
                listOfBlocks.Add(lineToBlockCreator(line));
            }
            return listOfBlocks;
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