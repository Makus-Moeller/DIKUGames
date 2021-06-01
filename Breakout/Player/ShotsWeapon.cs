using DIKUArcade.Math;
using DIKUArcade.Entities;
using System.IO;
using DIKUArcade.Graphics;
using DIKUArcade.Utilities;

namespace Breakout.Players {

    public class ShotsWeapon {
        public bool Active;
        public EntityContainer<PlayerShot> AllShots;

        public ShotsWeapon() {
            Active = false;
            AllShots = new EntityContainer<PlayerShot>();
        }

        public void Fire(Vec2F position, Vec2F extent) {
            if(Active) {
                AllShots.AddEntity(new PlayerShot(
                        new Vec2F(position.X + (extent.X / 2), 
                        position.Y), new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", 
                    "BulletRed2.png"))));
            }
        }

        public void Render() {
            if (Active) {
                AllShots.RenderEntities();
            }
        }

        public void Update() {
            if (Active) {
                AllShots.Iterate(shot => shot.UpdateShot());
            }
        }
    }
}