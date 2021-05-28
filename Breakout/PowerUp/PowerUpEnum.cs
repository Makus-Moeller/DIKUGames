using System;
namespace Breakout.PowerUpSpace {

    /// <summary>
    /// PowerUp types.
    /// </summary>
    public enum PowerUps {
        Elongate,
        SpeedBuff,
        ExtraLife,
        Split,
        Wall,
        Laser
    }

    /// <summary>
    /// Static class used as adaptor to convert strings to Statetypes and vise versa.
    /// </summary>
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
                case "Wall":
                    return PowerUps.Wall;
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
                case PowerUps.Wall:
                    return "Wall";
                case PowerUps.Laser:
                    return "Laser";
                default:
                    throw new ArgumentException("Not valid PowerUp");
            }
        }
    }
}