using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;


namespace Galaga {
    public class NoMove : IMovementStrategy {
        public float speedY { get; set ;}

        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            //Skal stå stille
        }

        public void MoveEnemy(Enemy enemy)
        {
            
        }
    }
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
            var newX = (float) (Math.Sin((2.00f*Math.PI*(startY-newY)) / p));
            enemy.Shape.Position = new Vec2F((startX + a * newX), (newY));
        }
    }
    //Statics klasse der står for opgradering af sværhedsgrader
    //Kan udvides til at ændre bevægelsesmønstre
    public static class IncreaseDifficulty {
        public static void IncreaseSpeedDown(IMovementStrategy squadron) {
            squadron.speedY += 0.0002f;
        }
    }
}