namespace Breakout.Levelloader {

    /// <summary>
    /// Reads a txtfil and returns useful content in array of strings.    
    /// </summary>
    public interface IFileReader {
        string[] ToStringArray(string File, string startingpoint, string breakpoint);
    }        

}