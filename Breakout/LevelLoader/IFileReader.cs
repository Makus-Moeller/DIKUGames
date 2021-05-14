namespace Breakout.Levelloader {

    ///Reads a txtfil and returns usefu content in array of strings
    public interface IFileReader {
        string[] ToStringArray(string File, string startingpoint, string breakpoint);
    }        

}