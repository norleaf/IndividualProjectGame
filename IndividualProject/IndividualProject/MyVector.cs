using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndividualProject
{
    public class MyVector
    {
        public float X;
        public float Y;

        public MyVector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public MyVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public MyVector Add(MyVector other)
        {
            return new MyVector(X+other.X,Y+other.Y);
        }

        public MyVector Subtract(MyVector other)
        {
            return new MyVector(X-other.X,Y-other.Y);
        }

        public MyVector Scale(float scalar)
        {
            return new MyVector(X*scalar,Y*scalar);
        }

        public double Magnitude()
        {
            return Math.Sqrt(X*X + Y*Y);
        }

        public double Angle()
        {
            return Math.Atan2(Y, X);
        }

        public double AngleDegree()
        {
            return Angle() * 180 / Math.PI;
        }

        public MyVector Normalize()
        {
            return Scale((float)(1/Magnitude()));
        }

        public static MyVector MagnitudeAndAngle(double r, double phi)
        {
            return new MyVector((float) (r * Math.Cos(phi)),(float)(r * Math.Sin(phi)));
        }
    }
}
