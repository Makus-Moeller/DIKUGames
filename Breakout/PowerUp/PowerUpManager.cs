using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.Events;
using DIKUArcade.Utilities;

namespace Breakout.PowerUpSpace
{
     /// <summary>
    /// PowerUp manager. Control events activated by powerup items.
    /// </summary>
    public class PowerUpManager : IGameEventProcessor {

        public EntityContainer<PowerUp> CurrentPowerUps;

        public PowerUpManager() {
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, this);
            CurrentPowerUps = new EntityContainer<PowerUp>();
        }

        /// <summary>
        /// Process gameevents that class is subscribed to. 
        /// </summary>
        /// <param name="gamevent">GameEvent that should be processed</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType ==  GameEventType.ControlEvent) {
                switch (gameEvent.Message) {
                    case "CreatePowerUp":
                        int randomBuff = new RandomNumberGenerator().GetNumber();
                        switch (randomBuff) {
                            case 1:
                                CurrentPowerUps.AddEntity(new PowerUp(new DynamicShape(new Vec2F(float.Parse(gameEvent.StringArg1), 
                                    float.Parse(gameEvent.StringArg2)), new Vec2F(1.0f/12.0f, 1.0f/24.0f), 
                                    new Vec2F(0.0f, -0.01f)), 
                                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "WidePowerUp.png")), PowerUps.Elongate));
                                break;
                            case 2:
                                CurrentPowerUps.AddEntity(new PowerUp(new DynamicShape(new Vec2F(float.Parse(gameEvent.StringArg1), 
                                    float.Parse(gameEvent.StringArg2)), 
                                    new Vec2F(1.0f/12.0f, 1.0f/24.0f), new Vec2F(0.0f, -0.01f)), 
                                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "SpeedPickUp.png")), PowerUps.SpeedBuff));
                                break;
                            case 3:
                                CurrentPowerUps.AddEntity(new PowerUp(new DynamicShape(new Vec2F(float.Parse(gameEvent.StringArg1), 
                                    float.Parse(gameEvent.StringArg2)), 
                                    new Vec2F(1.0f/12.0f, 1.0f/24.0f), new Vec2F(0.0f, -0.01f)), 
                                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "LifePickUp.png")), PowerUps.ExtraLife));
                                break;
                            case 4:
                                CurrentPowerUps.AddEntity(new PowerUp(new DynamicShape(new Vec2F(float.Parse(gameEvent.StringArg1), 
                                    float.Parse(gameEvent.StringArg2)), 
                                    new Vec2F(1.0f/12.0f, 1.0f/24.0f), new Vec2F(0.0f, -0.01f)), 
                                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "SplitPowerUp.png")), PowerUps.Split));
                                break;
                            case 5:
                                CurrentPowerUps.AddEntity(new PowerUp(new DynamicShape(new Vec2F(float.Parse(gameEvent.StringArg1), 
                                    float.Parse(gameEvent.StringArg2)), 
                                    new Vec2F(1.0f/12.0f, 1.0f/24.0f), new Vec2F(0.0f, -0.01f)), 
                                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "WallPowerUp.png")), PowerUps.Wall));
                                break;
                            case 6:
                                CurrentPowerUps.AddEntity(new PowerUp(new DynamicShape(new Vec2F(float.Parse(gameEvent.StringArg1), 
                                    float.Parse(gameEvent.StringArg2)), 
                                    new Vec2F(1.0f/12.0f, 1.0f/24.0f), new Vec2F(0.0f, -0.01f)), 
                                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "DamagePickUp.png")), PowerUps.Laser));
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void Update() {
            CurrentPowerUps.Iterate(powerUp => powerUp.Update());
        }

        /// <summary>
        /// Render images of the powerUps.
        /// </summary>
        public void RenderPowerUps() {
            CurrentPowerUps.Iterate(powerUp => powerUp.RenderPowerUp());
        }
    }
}
