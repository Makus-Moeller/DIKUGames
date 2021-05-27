using System;

namespace Breakout.PowerUpSpace {
    public class RandomNumberGenerator {
        private int number;
        private Random random;
        public RandomNumberGenerator() {
            random = new Random();
            number = random.Next(1, 5); 
        } 

       public int GetNumber() {
           return number;
       }
    }
}