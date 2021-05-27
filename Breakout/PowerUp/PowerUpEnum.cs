using System;
namespace Breakout.PowerUpSpace {
    public enum PowerUps {
        Elongate,
        SpeedBuff,
        ExtraLife
    }

    public static class PowerUpTransformer {
        public static PowerUps TransformStringToPowerUp(string powerUp) {
            switch (powerUp) {
                case "Elongate":
                    return PowerUps.Elongate;
                case "SpeedBuff":
                    return PowerUps.SpeedBuff;
                case "ExtraLife":
                    return PowerUps.ExtraLife;
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
                case PowerUps.ExtraLife:
                    return "ExtraLife";
                default:
                    throw new ArgumentException("Not valid PowerUp");
            }
        }
    }
}