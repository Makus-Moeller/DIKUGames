using System.Collections.Generic;
using DIKUArcade.Math;

namespace Breakout.Levelloader {
    
    /// <summary>
    /// The different types of blocks are defined by this class
    /// </summary>
    public class CharDefiners {
        public char character;
        public string imagePath;
        public string imagePath2;
        public bool powerUp;
        public bool hardened;
        public bool unbreakable;
        public  List<Vec2F> listOfPostions;

        public CharDefiners() {
            powerUp = false;
            hardened = false;
            unbreakable = false;
            character = ' ';
            listOfPostions = new List<Vec2F>();
        }
        public override string ToString()
        {
            return "char: " + character + "positions" + listOfPostions;
        }
    }

}