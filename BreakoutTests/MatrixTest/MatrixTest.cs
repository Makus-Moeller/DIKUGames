using NUnit.Framework;
using Breakout.Players;
using Breakout;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System.Diagnostics.Contracts;


namespace BreakoutTests {
    
    public class MatrixTest {
       
        private TwoDMatrix matrix; 
      
        [SetUp]
        public void Setup()
        {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            matrix = new TwoDMatrix();            
        }

        [Test]
        public void FillMatrixTest() {
            matrix.FillMatrix(1.0f, 2.0f, 3.0f, 4.0f);
            Assert.True(matrix.GetIndexOfArray(0,1) == 2.0f);
            Assert.True(matrix.GetIndexOfArray(0,0) == 1.0f);
            Assert.True(matrix.GetIndexOfArray(1,0) == 3.0f);
            Assert.True(matrix.GetIndexOfArray(1,1) == 4.0f); 
        } 

        [Test]
        public void multiplyByVectorTest() {
            matrix.FillMatrix(1.0f, 2.0f, 3.0f, 4.0f);
            Assert.True(matrix.multiplyByVector(new Vec2F(2.0f, 2.0f)).X == 6.0f);
            Assert.True(matrix.multiplyByVector(new Vec2F(2.0f, 2.0f)).Y == 14.0f);
        } 

        [Test]
        public void CreateRotationMatrixTest() {
            matrix.CreateRoationMatrix(45.0);
            Assert.True(matrix.GetIndexOfArray(0,1) == (float) -System.Math.Sin(45.0 * System.Math.PI/180.0)); 
        } 
    }
}
