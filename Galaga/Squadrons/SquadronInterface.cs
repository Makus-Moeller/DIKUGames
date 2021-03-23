using DIKUArcade.Entities;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using Galaga.MovementStrategy;

namespace Galaga.Squadrons {
    public interface ISquadron {
        EntityContainer<Enemy> Enemies {get;}
        IMovementStrategy strat {get;}
        int MaxEnemies {get; }
        void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemytStrides);
    }
}