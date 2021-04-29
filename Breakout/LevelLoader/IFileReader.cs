using System.IO;
using System;
using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout.Levelloader {
    ///Læste en txtfil og returnere det brugbare i et array af strings
    public interface IFileReader {
        string[] ToStringArray(string File, string startingpoint, string breakpoint);
    }        

}