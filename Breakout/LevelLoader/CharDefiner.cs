using System.IO;
using System;
using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Math;
namespace Breakout.Levelloader {
    public class CharDefiners {
        public char character;
        public string imagePath;
        public bool powerUp;
        public bool hardened;
        public bool unbreakable;
    

        public CharDefiners() {
            powerUp = false;
            hardened = false;
            unbreakable = false;
            character = ' ';
            imagePath = "";
        }
        public override string ToString()
        {
            return "char: " + character + " image: " + imagePath + " powerUp: " + powerUp.ToString() +  " harden: " + hardened.ToString() + " unbreakable:  " + unbreakable.ToString();
        }
    }

    
}