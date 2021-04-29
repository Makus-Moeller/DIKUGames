using System.IO;
using System;
using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
namespace Breakout.Levelloader {

    //Inderholder funktionalitet til at 
    public class LevelLoader {

        private IStringInterpreter stringInterpreter;
        private CharDefiners[] charDefiners;
        private IBlockCreator blockCreator;
        private List<AtomBlock> allBlocks;
        public void RenderBlocks() {
            foreach (AtomBlock block in allBlocks) {
                block.RenderEntity();
            }
        }
        //Hvis du vil sætte levelet skal du fortælle den :
        //Hvad det er baseret på, hvordan den skal læse og fortolke den
        //Og hvordan den skal genere entities ud fra det den læser.
        public void SetLevel(string file, IStringInterpreter interpreter, IBlockCreator blockCreator) {
            stringInterpreter = interpreter;
            this.blockCreator = blockCreator;
            stringInterpreter.ReadFile(file);
            charDefiners = stringInterpreter.CreateCharDefiners();
            allBlocks = blockCreator.CreateBlocks(charDefiners);
        }
    }




    public interface IBlockCreator {
        List<AtomBlock> CreateBlocks(CharDefiners[] charDefiners);
    }
    
    //Laver en liste af blocks
    //Fordi vi laver et interface er fordi det kan være man vil lave en 
    //generator der vil lave anden størelse eller hente billeder 
    //fran en anden stig
    public class BlockCreator : IBlockCreator {
       
        private List<AtomBlock> blocks = new List<AtomBlock>();  

        public List<AtomBlock> CreateBlocks(CharDefiners[] charDefiners)
        {
            
            foreach (CharDefiners charDefiner in charDefiners)
            {
                foreach (Vec2F position in charDefiner.listOfPostions)
                {
                    blocks.Add(new AtomBlock(new DIKUArcade.Entities.DynamicShape(position, 
                        new Vec2F(0.07f, 0.03f)), 
                        new Image(Path.Combine("..", "Breakout", "Assets", "Images", charDefiner.imagePath))
                        ));
                }
            }
            return blocks;
        }

        
    }
    public interface IStringInterpreter {
        void DefineSpecialAttributes();
        void GeneratePosition(CharDefiners[] arrayOfCharDefiners);
        void ReadFile(string File);
        CharDefiners[] CreateCharDefiners();
    }

    ///Laver Chardefinernse ved hjælp af en filereader.
    public class StringTxtInterpreter : IStringInterpreter {
        private IFileReader reader;
        private string[] legendData;
        private string[] mapData;
        private string[] metaData;
        private char powerup = ' ';
        private char harden = ' ';
        private char unbreakable = ' ';
        public CharDefiners[] arrayOfCharDefiners {get; private set;}
        public StringTxtInterpreter(IFileReader reader) {
            this.reader = reader;
            
        }
        public void ReadFile(string txtFile) {
            mapData = reader.ToStringArray(txtFile, "Map:", "Map/");
            legendData = reader.ToStringArray(txtFile, "Legend:", "Legend/");
            metaData = reader.ToStringArray(txtFile, "Meta:", "Meta/");
        }
        public void DefineSpecialAttributes() {
            int amountOfChars = legendData.Length;
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
        }

        public CharDefiners[] CreateCharDefiners() {
            DefineSpecialAttributes();
            int amountOfChars = legendData.Length;
            for (int i = 0; i < amountOfChars; i++) {
                arrayOfCharDefiners[i] = new CharDefiners();
                arrayOfCharDefiners[i].character = legendData[i][0];
                arrayOfCharDefiners[i].imagePath = legendData[i].Remove(0, 3);
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
            GeneratePosition(arrayOfCharDefiners);
            return arrayOfCharDefiners; 

        }

        public void GeneratePosition(CharDefiners[] arrayOfCharDefiners) {
            char currChar;
            for (int i = 0; i < mapData.Length; i++) {
                for (int j = 0; j < 12; j++) { 
                    if ((currChar = mapData[i][j]) != '-') {
                        foreach (CharDefiners charDefiner in arrayOfCharDefiners) {                   
                            if (currChar == charDefiner.character) {
                                charDefiner.listOfPostions.Add(new Vec2F(0.0f + (float)j * (1.0f/12.0f), 1.0f - (float)i * (1.0f/mapData.Length)));
                            }
                        }
                    }
                }
            }
        }
    }



    ///Læste en txtfil og returnere det brugbare i et array af strings

    public interface IFileReader {
        string[] ToStringArray(string File, string startingpoint, string breakpoint);
    }        
    public class StreamReaderClass : IFileReader {
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
        
        public string[] ToStringArray(string File, string startingpoint, string breakpoint) {
            string[] stringArray = new string[countNumberOfValidLines(File, startingpoint, breakpoint)];
            string line;
            int lineNumber = 0;
            System.IO.StreamReader file =
                new System.IO.StreamReader(File);
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