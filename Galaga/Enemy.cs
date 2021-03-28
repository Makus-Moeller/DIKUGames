using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Enemy : Entity {
        public bool isEnraged {get; private set;}
        public int hitpoints {get; private set;}
        private IBaseImage redImage;
        public readonly Vec2F startposition;
        public static int TOTAL_ENEMIES{get; private set;}

        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enemyStridesRed) : 
            base(shape, image) {
                hitpoints = 5;
                redImage = enemyStridesRed;
                startposition = shape.Position;
                isEnraged = false;
                TOTAL_ENEMIES++;
        }
        public bool Enrage() {
            hitpoints--;
            if (hitpoints <= 3 && hitpoints > 0) {
                this.Image = redImage;
                isEnraged = true;
                return false;
            }
            else if (hitpoints == 0) {
                this.DeleteEntity();
                TOTAL_ENEMIES--;
                return true;
            }
            else
                return false;
        }
        public static void ResetEnemyCount() {
            TOTAL_ENEMIES = 0;
        }
    } 
}