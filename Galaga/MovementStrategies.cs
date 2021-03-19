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

            // Lav det om til enemy.shape.position
            //enemy.Shape.MoveY(s);
            var newY = enemy.Shape.Position.Y - s;
            var newX = (float) (Math.Sin((2.00f*Math.PI*(startY-newY)) / p));
            //var radians = Math.PI * ToDouble / 180.0;
            //var sin = (float)(Math.Sin(ToDouble));
            enemy.Shape.Position = new Vec2F((startX + a * newX), (newY));
        }
    }
}