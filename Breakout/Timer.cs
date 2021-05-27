using Breakout.Levelloader;
using DIKUArcade.Timers;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout{

    /// sættes ind i setLevel i levelloader. Timer tæller bare ned og sender et gamestateevent når tiden er udløbet.
    //Ansvar for at rendere tiden finded i en anden klasse. Tiden må tilgås med en anden klasse. 
    /// <summary>
    /// Handles time for levelloader, if metadata contains timedata.
    /// </summary>
    public class Timer {
        private Text display;
        private double levelTime;
        private int timeRemaining;

        public Timer() {
            levelTime = 0.0D;
            display = new Text("Time left: " + timeRemaining.ToString(), 
                new Vec2F(0.7f, 0.01f), new Vec2F(0.2f, 0.2f));
        }
        
        /// <summary>
        /// Checks whether stringinterpreter contain timedata.
        /// </summary>
        /// <param name="stringIntepreter">StringInteprerter contains datainfo</param>
        public double SetLevelTime(IStringInterpreter stringInterpreter) {
            levelTime = 0.0;
            string[] allMeta = stringInterpreter.GetMetaData();
            foreach (string line in allMeta) {
                if (line[0] == 'T') {
                    levelTime = double.Parse(line.Substring(6, line.Length-6)); 
                    timeRemaining = (int) levelTime;
                }
            }
            return levelTime;
        }

        public void RenderTime() {
            display.SetColor(new Vec3I(191, 0, 255));
            if (levelTime > 0.0D) {
                display.RenderText();
            }
        }

        public void UpdateTimeRemaining() {
            timeRemaining =  (int) (levelTime - StaticTimer.GetElapsedSeconds());
            display.SetText("Time left: " + timeRemaining.ToString()); 
            RenderTime();
        }
        
        /// <summary>
        /// Tells if time is up.
        /// </summary>
        public bool IsTimesUp() {
            if (levelTime > 0.0 && StaticTimer.GetElapsedSeconds() > levelTime) {
                return true;
            }
            else return false;
        }
    } 
}