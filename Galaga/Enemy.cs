using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Galaga {
    public class Enemy : Entity {
        private int hitpoints;
        private IBaseImage redImage;
        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enemyStridesRed) : base(shape, image) {
            hitpoints = 10;
            redImage = enemyStridesRed;
        }
        public bool Enrage (){
            hitpoints--;
            if (hitpoints <= 3 && hitpoints > 0) {
                this.Image = redImage;
                return false;
            }
            else if (hitpoints == 0) {
                this.DeleteEntity();
                return true;
            }
            else
                return false;
        }
    } 
}