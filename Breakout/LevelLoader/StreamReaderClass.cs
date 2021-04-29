using System.IO;
using System;
using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout.Levelloader {

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