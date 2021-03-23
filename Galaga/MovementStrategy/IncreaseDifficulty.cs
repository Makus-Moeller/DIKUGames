
namespace Galaga.MovementStrategy
{
    //Static class which increase level of game 
    public static class IncreaseDifficulty {
        public static void IncreaseSpeedDown(IMovementStrategy strategy) {
            strategy.speedY += 0.0002f;
        }
    }
}
