using System;
namespace Breakout.Levelloader {
    
    /// <summary>
    /// Reads a file and converts it to
    /// an array containging only the information
    /// you ask for
    /// </summary>
    public class StreamReaderClass : IFileReader {
        private int CountNumberOfValidLines(string txtFile, 
            string startingpoint, string breakpoint) {
            int numberOfLines = 0;
            string line;
            System.IO.StreamReader file =
                new System.IO.StreamReader(txtFile);
            while ((line = file.ReadLine()) != startingpoint && !file.EndOfStream) {
            }
            while((line = file.ReadLine()) != breakpoint && !file.EndOfStream) {    
                numberOfLines++;
            }
            return numberOfLines;
        }
        
        public string[] ToStringArray(string File, string startingpoint, string breakpoint) {
            try {
                string[] stringArray = new string[CountNumberOfValidLines(File, 
                    startingpoint, breakpoint)];
            string line;
            int lineNumber = 0;
            System.IO.StreamReader file =
                new System.IO.StreamReader(File);
            while (startingpoint != (line = file.ReadLine()) && !file.EndOfStream) {
            }
            while((line = file.ReadLine()) != breakpoint && !file.EndOfStream)  
            {  
                if (line == startingpoint) {}
                else {
                stringArray[lineNumber] = line;    
                lineNumber++;
                }
            }
            return stringArray;
            }
            catch (System.IO.FileNotFoundException) {
                string[] emptyArr  = {};
                return emptyArr;
            }
        }
    }
}