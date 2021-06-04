using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using System.IO;
using System.Diagnostics.Contracts;
using Breakout.Levelloader;
using DIKUArcade.Utilities;
using System.Collections.Generic;
using Breakout.Balls;
namespace BreakoutTests {

    [TestFixture]    
    public class CollisionHandlerFullWhiteBoxTest {
        CollisionHandlerStud collisionHandler;
        AtomBlock atomblock;
        Player player;
        Ball ball;

        [SetUp]
        public void Setup() {
            collisionHandler = new CollisionHandlerStud();
            ball = new Ball(new DynamicShape(new Vec2F(0.50f, 0.08f), new Vec2F(0.04f, 0.04f), 
                    new Vec2F(0.005f, 0.015f)),
                    new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "ball2.png")));
            atomblock = new AtomBlock(new DynamicShape(new Vec2F(0.50f, 0.08f), 
                            new Vec2F(1.0f/12.0f, 1.0f/24f)), 
                            new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", 
                                "green-block.png")));
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.08f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.Combine(FileIO.GetProjectPath(), "Assets", "Images", "player.png")), 
                    new RegularBuffState()); 
        }

        [Test]
        public void BreakEarlyPath() {
            CollisionData collisionData = new CollisionData();
            collisionData.Collision = false;
            Assert.AreEqual(0, collisionHandler.HandleCollision(ball, atomblock, collisionData));
            
        }

        [Test]
        public void NullCollidableAndSwitchArgumentPath() {
            CollisionData collisionData = new CollisionData();
            collisionData.Collision = true;
            Assert.AreEqual(2, collisionHandler.HandleCollision(player, ball, collisionData));
        }
        
        [Test]
        public void CollidablePath() {
            CollisionData collisionData = new CollisionData();
            collisionData.Collision = true;
            Assert.AreEqual(1, collisionHandler.HandleCollision(ball, atomblock, collisionData));
        
        }
    }
}