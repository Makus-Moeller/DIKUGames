using Breakout.Levelloader;
using DIKUArcade.Timers;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Utilities;
using System.IO;


namespace Breakout{

    /// sættes ind i setLevel i levelloader. Timer tæller bare ned og sender et gamestateevent når tiden er udløbet.
    //Ansvar for at rendere tiden finded i en anden klasse. Tiden må tilgås med en anden klasse. 
    /// <summary>
    /// Handles time for levelloader, if metadata contains timedata.
    /// </summary>
    public class Timer {
        private Text display;
        private Text timeleft;  
        private double levelTime;
        private int timeRemaining;

        public Timer() {
            levelTime = 0.0D;
            timeleft = new Text("Time Left: ", 
                new Vec2F(0.4f, 0.834f), new Vec2F(0.25f, 0.16f));
            display = new Text(timeRemaining.ToString(), 
                new Vec2F(0.6f, 0.834f), new Vec2F(0.25f, 0.16f));
            
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
            display.SetColor(new Vec3I(255, 180, 25));
            timeleft.SetColor(new Vec3I(255, 180, 25));
            if (levelTime > 0.0D) {
                timeleft.RenderText();
                display.RenderText();
            }
        }

        public void UpdateTimeRemaining() {
            timeRemaining =  (int) (levelTime - StaticTimer.GetElapsedSeconds());
            display.SetText(timeRemaining.ToString()); 
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