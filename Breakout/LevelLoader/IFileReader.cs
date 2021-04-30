namespace Breakout.Levelloader {

    ///Læste en txtfil og returnere det brugbare i et array af strings
    public interface IFileReader {
        string[] ToStringArray(string File, string startingpoint, string breakpoint);
    }        

}