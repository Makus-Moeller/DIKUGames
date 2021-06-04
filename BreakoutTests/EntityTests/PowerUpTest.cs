using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Levelloader;
using Breakout.Balls;
using Breakout.PowerUpSpace;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Utilities;
using DIKUArcade.Timers;
using System.IO;
using System.Diagnostics.Contracts;
using Breakout.Blocks;
using System;
using System.Collections.Generic;

namespace BreakoutTests {
    [TestFixture]
    public class PowerUpTest {
        private PowerUp ElongatePowerUp;
        private PowerUp SpeedPowerUp;
        private PowerUp ExtraLifePowerUp;
        private PowerUp SplitPowerUp;
        private PowerUp LaserPowerUp;
        private Player player; 
        private BallManager ballManager;
        
        public PowerUpTest() {
            ElongatePowerUp =  
                new PowerUp(new DynamicShape(new Vec2F(0.2f, 0.22f), 
                    new Vec2F(1.0f/12.0f, 1.0f/24.0f), 
                    new Vec2F(0.0f, -0.01f)), 
                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets",
                        "Images", "WidePowerUp.png")), PowerUps.Elongate);

            SpeedPowerUp =  
                new PowerUp(new DynamicShape(new Vec2F(0.2f, 0.22f), 
                    new Vec2F(1.0f/12.0f, 1.0f/24.0f), 
                    new Vec2F(0.0f, -0.01f)), 
                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets",
                        "Images", "SpeedPickUp.png")), PowerUps.SpeedBuff);
            
            ExtraLifePowerUp =  
                new PowerUp(new DynamicShape(new Vec2F(0.2f, 0.22f), 
                    new Vec2F(1.0f/12.0f, 1.0f/24.0f), 
                    new Vec2F(0.0f, -0.01f)), 
                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets",
                        "Images", "SpeedPickUp.png")), PowerUps.ExtraLife);

            SplitPowerUp =  
                new PowerUp(new DynamicShape(new Vec2F(0.2f, 0.22f), 
                    new Vec2F(1.0f/12.0f, 1.0f/24.0f), 
                    new Vec2F(0.0f, -0.01f)), 
                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets",
                        "Images", "SpeedPickUp.png")), PowerUps.Split);

            LaserPowerUp =  
                new PowerUp(new DynamicShape(new Vec2F(0.2f, 0.22f), 
                    new Vec2F(1.0f/12.0f, 1.0f/24.0f), 
                    new Vec2F(0.0f, -0.01f)), 
                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets",
                        "Images", "SpeedPickUp.png")), PowerUps.Laser);

            ballManager = new BallManager();
        }

        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "player.png")), 
                new RegularBuffState()); 
        }

        [Test]
        public void TestElongatePowerUp() {
            Assert.True(ElongatePowerUp.ThisPowerUp == PowerUps.Elongate);
            Assert.True(player.playerBuffState is RegularBuffState);
            player.ProcessEvent(new GameEvent{EventType = GameEventType.ControlEvent, 
                    Message = "HandlePowerUp",
                    StringArg1 = PowerUpTransformer.TransformPowerUpToString(PowerUps.Elongate)});
            
            Assert.True(player.playerBuffState is ElongateBuffState);
        }

        [Test]
        public void TestSpeedPowerUp() {
            Assert.True(SpeedPowerUp.ThisPowerUp == PowerUps.SpeedBuff);
            Assert.True(player.playerBuffState is RegularBuffState);
            player.ProcessEvent(new GameEvent{EventType = GameEventType.ControlEvent, 
                    Message = "HandlePowerUp",
                    StringArg1 = PowerUpTransformer.TransformPowerUpToString(PowerUps.SpeedBuff)});
            
            Assert.True(player.playerBuffState is SpeedBuffState);
        }

        [Test]
        public void TestExtraLifePowerUp() {
            Assert.True(ExtraLifePowerUp.ThisPowerUp == PowerUps.ExtraLife);
            Assert.AreEqual(player.playerLives.Lives, 4);
            player.ProcessEvent(new GameEvent{EventType = GameEventType.ControlEvent, 
                    Message = "HandlePowerUp",
                    StringArg1 = PowerUpTransformer.TransformPowerUpToString(PowerUps.ExtraLife)});
            
            Assert.AreEqual(player.playerLives.Lives, 5);
        }

         [Test]
        public void TestLaserPowerUp() {
            Assert.True(LaserPowerUp.ThisPowerUp == PowerUps.Laser);
            Assert.False(player.Weapon.Active);
            player.ProcessEvent(new GameEvent{EventType = GameEventType.ControlEvent, 
                    Message = "HandlePowerUp",
                    StringArg1 = PowerUpTransformer.TransformPowerUpToString(PowerUps.Laser)});
            
            Assert.True(player.Weapon.Active);
        }

        [Test]
        public void TestSplitPowerUp() {
            Assert.True(ballManager.IsEmpty());
            ballManager.AddBall(new Vec2F(0.50f, 0.08f), new Vec2F(0.005f, 0.015f));
            Assert.False(ballManager.IsEmpty());
            ballManager.ProcessEvent(new GameEvent{EventType = GameEventType.ControlEvent, 
                Message = "HandlePowerUp",
                StringArg1 = PowerUpTransformer.TransformPowerUpToString(PowerUps.Split)});
            
            Assert.AreEqual(3, ballManager.allBalls.CountEntities());
        }
    }
}