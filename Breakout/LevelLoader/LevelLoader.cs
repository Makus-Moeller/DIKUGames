using System.IO;
using System;
namespace Breakout.Levelloader {
    public class Levelloader {




    }

    public class StreamReaderClass {
        private int countNumberOfValidLines(string txtFile) {
            int numberOfLines = 0;
            string line;
            System.IO.StreamReader file =
                new System.IO.StreamReader(txtFile);  
            while((line = file.ReadLine()) != "Map/")  
            {    
                numberOfLines++;
            }
            return numberOfLines;
        }
        

        public string[] txtToArray(string txtFile) {
            string[] stringArray = new string[countNumberOfValidLines(txtFile)-1];
            string line;
            int lineNumber = 0;
            System.IO.StreamReader file =
                new System.IO.StreamReader(txtFile);
            while((line = file.ReadLine()) != "Map/")  
            {  
                if (line == "Map:") {}
                else {
                stringArray[lineNumber] = line;    
                lineNumber++;
                }
            }
            return stringArray;
        }
    }
}