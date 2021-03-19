using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;


namespace Galaga {
    public class NoMove : IMovementStrategy {
        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            
        }

        public void MoveEnemy(Enemy enemy)
        {
            
        }
    }
    public class Down : IMovementStrategy {
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy => MoveEnemy(enemy));
        }

        public void MoveEnemy(Enemy enemy)
        {
            enemy.Shape.MoveY(-0.001f);
        }
    }
    public class ZigZagDown : IMovementStrategy {
    
        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            enemies.Iterate(enemy => MoveEnemy(enemy));
        }

        public void MoveEnemy(Enemy enemy)
        {
            float startX = enemy.startposition.X;
            float startY = enemy.startposition.Y;
            float s = 0.0003f;
            float p = 0.045f;
            float a = 0.05f;
            enemy.Shape.MoveY(-s);
            var ToDouble = (double)((2.00f*Math.PI*(startY-enemy.Shape.Position.Y)) / p);
            var radians = Math.PI * ToDouble / 180.0;
            var sin = (float)(Math.Sin(radians));
            enemy.Shape.MoveX((startX + a * sin) - enemy.Shape.Position.X);
        }
    }
}