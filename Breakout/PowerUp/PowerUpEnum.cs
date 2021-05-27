using System;
namespace Breakout.PowerUpSpace {
    public enum PowerUps {
        Elongate,
        SpeedBuff,
        ExtraLife,
        Split,
        Laser
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
                case "Split":
                    return PowerUps.Split;
                 case "Laser":
                    return PowerUps.Laser;
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
                case PowerUps.Split:
                    return "Split";
                case PowerUps.Laser:
                    return "Laser";
                default:
                    throw new ArgumentException("Not valid PowerUp");
            }
        }
    }
}