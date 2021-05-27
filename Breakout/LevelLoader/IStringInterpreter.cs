namespace Breakout.Levelloader {
    public interface IStringInterpreter {
        string[] GetMetaData();
        void ReadFile(string File);
        CharDefiners[] CreateCharDefiners();
    }
}   