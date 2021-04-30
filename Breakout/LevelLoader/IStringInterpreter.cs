namespace Breakout.Levelloader {
    public interface IStringInterpreter {
        void ReadFile(string File);
        CharDefiners[] CreateCharDefiners();
    }
}   