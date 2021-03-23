using DIKUArcade.Entities;

namespace Galaga.MovementStrategy
{
    public class Down : IMovementStrategy {
        public float speedY { get; set ;}
        public Down()
        {
            speedY = 0.0002f;
        }
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy => MoveEnemy(enemy));
        }

        public void MoveEnemy(Enemy enemy)
        {
            if (enemy.isEnraged) {
                enemy.Shape.MoveY(-(speedY + 0.0004f));
            }
            else {   
                enemy.Shape.MoveY(-speedY);
            }
        }
    }
}