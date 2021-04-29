using System.IO;
using System;
using System.Collections.Generic;
using Breakout.Blocks;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout.Levelloader {
    public interface IStringInterpreter {
        void DefineSpecialAttributes();
        void GeneratePosition(CharDefiners[] arrayOfCharDefiners);
        void ReadFile(string File);
        CharDefiners[] CreateCharDefiners();
    }
}   