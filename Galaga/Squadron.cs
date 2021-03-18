using DIKUArcade.Entities;
using System.Collections.Generic;
using DIKUArcade.Graphics;
namespace Galaga {
    public interface ISquadron {
        EntityContainer<Enemy> Enemies {get;}
        int MaxEnemies {get; }
        void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemytStrides);
    }
}