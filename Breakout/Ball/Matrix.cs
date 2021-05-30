using DIKUArcade.Math;
using System;

namespace Breakout.Balls {

    /// <summary>
    /// Matrix class used to change direction of ball.
    /// </summary>
    public class TwoDMatrix {
        public float[,] matrix;

        public TwoDMatrix() {
            matrix = new float[2,2];
        }

        public void FillMatrix(float oneOne, float oneTwo, float twoOne, float twoTwo) {
            matrix[0,0] = oneOne;
            matrix[0,1] = oneTwo;
            matrix[1,0] = twoOne;
            matrix[1,1] = twoTwo;
        }

        public Vec2F multiplyByVector(Vec2F vector) {
            return new Vec2F(matrix[0,0] * vector.X + matrix[0,1] * vector.Y, matrix[1,0] * vector.X + matrix[1,1] * vector.Y);
        }

        /// <summary>
        /// Rotational matrix used to rotate direction.
        /// </summary>
        /// <param name="factor">The angle to rotate with</param>
        public void CreateRoationMatrix(double factor) {
            matrix[0,0] = (float) Math.Cos(factor * Math.PI/180.0);
            matrix[0,1] = (float) -Math.Sin(factor * Math.PI/180.0);
            matrix[1,0] = (float) Math.Sin(factor * Math.PI/180.0);
            matrix[1,1] = (float) Math.Cos(factor * Math.PI/180.0);
        }

        //Only used for testing
        public float GetIndexOfArray(int i, int j)
        {
            return matrix[i,j];
        }
    }
}