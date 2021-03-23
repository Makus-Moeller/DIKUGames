using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    public interface IMovementStrategy {
        float speedY {get; set;}
        void MoveEnemy(Enemy enemy);
        void MoveEnemies(EntityContainer<Enemy> enemies);
    }
}
