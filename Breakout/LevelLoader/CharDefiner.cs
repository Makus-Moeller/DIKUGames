using System.Collections.Generic;
using DIKUArcade.Math;
namespace Breakout.Levelloader {

    public class CharDefiners {
        public char character;
        public string imagePath;
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