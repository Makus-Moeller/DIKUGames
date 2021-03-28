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

namespace GalagaTests
{
    public class TestSquadron {
        
        private List<Image> enemyStridesRed;
        private List<Image> images;
        private DiagonaleSquad diagonal;
        private VerticalSquad vertical;
        private KvadratiskSquad kvadratisk;
       

        [SetUp]
        public void Setup() {
            DIKUArcade.Window.CreateOpenGLContext();

            //Images
            enemyStridesRed = ImageStride.CreateStrides(
                2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
            images = ImageStride.CreateStrides(
                4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png")); 
            //Enemies
            diagonal = new DiagonaleSquad(4, new ZigZagDown());
            vertical = new VerticalSquad(4, new Down());
            kvadratisk = new KvadratiskSquad(4, new ZigZagDown());
        }

        [Test]
        public void TestDiagonaleSquad() {
            diagonal.CreateEnemies(images, enemyStridesRed);
            Assert.AreEqual(4, diagonal.MaxEnemies);
            Assert.AreEqual(4, diagonal.Enemies.CountEntities());
            Assert.AreEqual("Strategy: Galaga.MovementStrategy.ZigZagDown", diagonal.ToString());
            
            
        }

        [Test]
        public void TestVerticalSquad() {
            vertical.CreateEnemies(images, enemyStridesRed);
            Assert.AreEqual(4, vertical.MaxEnemies);
            Assert.AreEqual(4, vertical.Enemies.CountEntities());
            Assert.AreEqual("Strategy: Galaga.MovementStrategy.Down", vertical.ToString());
            
        }

        [Test]
        public void TestKvadratiskSquad() {
            kvadratisk.CreateEnemies(images, enemyStridesRed);
            Assert.AreEqual(4, kvadratisk.MaxEnemies);
            Assert.AreEqual(4, kvadratisk.Enemies.CountEntities());
            Assert.AreEqual("Strategy: Galaga.MovementStrategy.ZigZagDown", kvadratisk.ToString());
        }
    }
}