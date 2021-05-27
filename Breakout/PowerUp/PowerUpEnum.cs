using System;
namespace Breakout.PowerUpSpace {
    public enum PowerUps {
        Elongate,
        SpeedBuff
    }

    public static class PowerUpTransformer {
        public static PowerUps TransformStringToPowerUp(string powerUp) {
            switch (powerUp) {
                case "Elongate":
                    return PowerUps.Elongate;
                case "SpeedBuff":
                    return PowerUps.SpeedBuff;
                default:
                    throw new ArgumentException("Not valid PowerUp"); 
            }
        }      

        public static string TransformPowerUpToString(PowerUps powerUp) {
            switch (powerUp) {
                case PowerUps.Elongate:
                    return "Elongate";
                case PowerUps.SpeedBuff:
                    return "SpeedBuff";
                default:
                    throw new ArgumentException("Not valid PowerUp");
            }
        }
    }
}