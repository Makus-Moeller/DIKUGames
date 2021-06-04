using DIKUArcade.Math;
using System;
namespace Breakout.Levelloader {

    /// <summary>
    /// Interprets the read file
    /// </summary>
    public class StringTxtInterpreter : IStringInterpreter {
        private IFileReader reader;
        private string[] legendData;
        private string[] mapData;
        public string[] metaData;
        private char powerup = ' ';
        private char harden = ' ';
        public char unbreakable = ' ';
        public CharDefiners[] arrayOfCharDefiners {get; private set;}
        public StringTxtInterpreter(IFileReader reader) {
            this.reader = reader;
            
        }
        public void ReadFile(string txtFile) {
            mapData = reader.ToStringArray(txtFile, "Map:", "Map/");
            legendData = reader.ToStringArray(txtFile, "Legend:", "Legend/");
            metaData = reader.ToStringArray(txtFile, "Meta:", "Meta/");
        }
        private void DefineSpecialAttributes() {
            int amountOfChars = legendData.Length;
            arrayOfCharDefiners = new CharDefiners[amountOfChars];
            for (int i = 0; i < metaData.Length; i++) {
                Console.WriteLine(metaData[i][0]);
                if (metaData[i][0] == 'P') {
                    powerup = metaData[i][9];
                    Console.WriteLine("powerup: " + metaData[i]);
                }
                if (metaData[i][0] == 'H') {
                    harden = metaData[i][10];
                    Console.WriteLine("harden: " + metaData[i]);
                }
                if (metaData[i][0] == 'U') {
                    unbreakable = metaData[i][13];
                    Console.WriteLine("unbreakable: " + metaData[i]);
                }
            }
        }

        /// <summary>
        /// Main function of class. 
        /// defines all the different chars
        /// and assigns positions
        /// </summary>
        public CharDefiners[] CreateCharDefiners() {
            DefineSpecialAttributes();
            int amountOfChars = legendData.Length;
            for (int i = 0; i < amountOfChars; i++) {
                arrayOfCharDefiners[i] = new CharDefiners();
                if (legendData[i].Length != 0) {
                    arrayOfCharDefiners[i].character = legendData[i][0];
                    arrayOfCharDefiners[i].imagePath = legendData[i].Remove(0, 3);
                }
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
        
        /// <summary>
        /// adds the position of the chars based on the mapData
        /// </summary>
        /// <param name="arrayOfCharDefiners">All the found chars</param>
        private void GeneratePosition(CharDefiners[] arrayOfCharDefiners) {
            char currChar;
            for (int i = 0; i < mapData.Length; i++) {
                for (int j = 0; j < 12; j++) { 
                    if ((currChar = mapData[i][j]) != '-') {
                        foreach (CharDefiners charDefiner in arrayOfCharDefiners) {                   
                            if (currChar == charDefiner.character) {
                                charDefiner.listOfPostions.Add(
                                    new Vec2F(0.0f + (float)j * (1.0f/12.0f), 
                                        1.0f - (float)i * (1.0f/mapData.Length)));
                            }
                        }
                    }
                }
            }
        }

        public string[] GetMetaData()
        {
            return metaData;
        }
    }
}