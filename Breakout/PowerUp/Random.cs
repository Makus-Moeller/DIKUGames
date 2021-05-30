using System;

namespace Breakout.PowerUpSpace {

    /// <summary>
    /// Random number generator to generate random powerUps.
    /// </summary>
    public class RandomNumberGenerator {
        private int number;
        private Random random;
        public RandomNumberGenerator() {
            random = new Random();
            number = random.Next(1, 7); 
        } 

        /// <summary>
        /// Gets a random number.
        /// </summary>
        public int GetNumber() {
            return number;
        }
    }
}