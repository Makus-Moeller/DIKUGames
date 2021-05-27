using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using DIKUArcade.Events;
using Breakout.Players;
using Breakout.Levelloader;
using DIKUArcade.Input;
using DIKUArcade.Timers;
using Breakout.Blocks;

namespace Breakout.PowerUpSpace {

    public class PowerUpManager : IGameEventProcessor {

        public EntityContainer<PowerUp> CurrentPowerUps;

        public PowerUpManager() {
            BreakoutBus.GetBus().Subscribe(GameEventType.ControlEvent, this);
            CurrentPowerUps = new EntityContainer<PowerUp>();
        }
        
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType ==  GameEventType.ControlEvent) {
                switch (gameEvent.Message) {
                    case "CreatePowerUp":
                        int randomBuff = new RandomNumberGenerator().GetNumber();
                        Console.WriteLine(randomBuff);
                        switch (randomBuff) {
                            case 1 :
                                CurrentPowerUps.AddEntity(new PowerUp(new DynamicShape(new Vec2F(float.Parse(gameEvent.StringArg1), float.Parse(gameEvent.StringArg2)), new Vec2F(1.0f/12.0f, 1.0f/24.0f), new Vec2F(0.0f, -0.01f)), new Image(Path.Combine("..", "Breakout", "Assets", "Images", "WidePowerUp.png")), PowerUps.Elongate));
                                break;
                            case 2 :
                                CurrentPowerUps.AddEntity(new PowerUp(new DynamicShape(new Vec2F(float.Parse(gameEvent.StringArg1), float.Parse(gameEvent.StringArg2)), new Vec2F(1.0f/12.0f, 1.0f/24.0f), new Vec2F(0.0f, -0.01f)), new Image(Path.Combine("..", "Breakout", "Assets", "Images", "SpeedPickUp.png")), PowerUps.SpeedBuff));
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

        public void RenderPowerUps() {
            CurrentPowerUps.Iterate(powerUp => powerUp.RenderPowerUp());
        }
    }
}
