namespace Breakout.Levelloader {
    public interface IStringInterpreter {
        void DefineSpecialAttributes();
        void GeneratePosition(CharDefiners[] arrayOfCharDefiners);
        void ReadFile(string File);
        CharDefiners[] CreateCharDefiners();
    }
}   