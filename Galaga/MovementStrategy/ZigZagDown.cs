using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;


namespace Galaga.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        public ZigZagDown() {
            speedY = 0.0003f;
        }

        public float speedY { get; set ;}
    
        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            enemies.Iterate(enemy => MoveEnemy(enemy));
        }

        public void MoveEnemy(Enemy enemy)
        {
            float startX = enemy.startposition.X;
            float startY = enemy.startposition.Y;
            float s = speedY;
            float p = 0.045f;
            float a = 0.05f;
            
            if (enemy.isEnraged) {
                s += 0.0005f;
            }
            var newY = enemy.Shape.Position.Y - s;
            var newX = (float) (Math.Sin((2.00f * Math.PI * (startY - newY)) / p));
            enemy.Shape.Position = new Vec2F((startX + a * newX), (newY));
        }
    }
}
