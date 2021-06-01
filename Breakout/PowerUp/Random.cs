using System;

namespace Breakout.PowerUpSpace {

    /// <summary>
    /// Random number generator to generate random powerUps.
    /// </summary>
    public class RandomNumberGenerator {
        private int number;
        private Random random;
        /// <summary>
        /// Process gameevents that class is subscribed to.
        /// </summary>
        /// <param name="i">max number in interval to get random number from</param>
        public RandomNumberGenerator(int i) {
            random = new Random();
            number = random.Next(1, i+1); 
        } 

        /// <summary>
        /// Gets a random number.
        /// </summary>
        public int GetNumber() {
            return number;
        }
    }
}