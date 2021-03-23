using DIKUArcade.Entities;


namespace Galaga.MovementStrategy
{
    public class NoMove : IMovementStrategy {
        public float speedY { get; set ;}

        public void MoveEnemies(EntityContainer<Enemy> enemies) {}

        public void MoveEnemy(Enemy enemy){}
    }
}