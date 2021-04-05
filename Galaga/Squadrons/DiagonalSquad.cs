using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;
using Galaga.MovementStrategy;

namespace Galaga.Squadrons {
    public class DiagonaleSquad : ISquadron {
        public IMovementStrategy strat {get;}
        public DiagonaleSquad(int numOfEnemies, IMovementStrategy moveStrat) 
        {
            strat = moveStrat;
            MaxEnemies = numOfEnemies;
        }

        public EntityContainer<Enemy> Enemies {get; private set;}
        public int MaxEnemies {get;}
        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemytStrides) {
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            for(int i = 0; i < MaxEnemies; i++) {
                Enemies.AddEntity(new Enemy(new DynamicShape(
                    new Vec2F (Math.Min(0.3f + (float)i * 0.1f, 0.91f), 
                    Math.Max(0.5f + (float)i * 0.1f, 0.5f)), new Vec2F (0.1f , 0.1f)), 
                    new ImageStride(80, enemyStrides), 
                    new ImageStride (80, alternativeEnemytStrides)));
            }        
        }   

        //overrides ToString. Used in Nunittesting 
        public override string ToString() {
            return string.Format("Strategy: {0}", 
                strat);
        }
    }
}