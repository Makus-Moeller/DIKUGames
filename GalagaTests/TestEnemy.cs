using NUnit.Framework;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using DIKUArcade.EventBus;
using Galaga;
using System.Collections.Generic;
using Galaga.Squadrons;
using Galaga.MovementStrategy;
using DIKUArcade.Physics;

namespace GalagaTests {

    public class TestEnemy {
        
        private Enemy enemy;
        

        [SetUp]
        public void Setup()
        {
            DIKUArcade.Window.CreateOpenGLContext();
            enemy = new Enemy(
                new DynamicShape(new Vec2F (0.3f, 0.91f), new Vec2F (0.1f , 0.1f)), 
                new Image(Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png")), 
                new Image(Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png")));
        }

        [Test]
        public void TestEnrage() {
            Assert.AreEqual(5, enemy.hitpoints);
            Assert.False(enemy.isEnraged);
            enemy.Enrage();
            Assert.AreEqual(4, enemy.hitpoints);
            enemy.Enrage();
            Assert.AreEqual(3, enemy.hitpoints);
            Assert.True(enemy.isEnraged);
            enemy.Enrage();
            enemy.Enrage();
            enemy.Enrage();
            Assert.True(enemy.IsDeleted());
        }

        [Test]
        public void TestResetEnemyCount() {
            Assert.AreNotEqual(0, Enemy.TOTAL_ENEMIES);
            Enemy.ResetEnemyCount();
            Assert.AreEqual(0, Enemy.TOTAL_ENEMIES);
        }
    }
}