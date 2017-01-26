using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PathFinder.Astar
{
    /// <summary>    
    /// Author: Roy Triesscheijn (http://www.roy-t.nl)
    /// Point3D class mimics some of the Microsoft.Xna.Framework.Vector3
    /// but uses Int32's instead of floats.
    /// </summary>
    public class Point2D
    {
        public int X;
        public int Y;
        //public int Z;        

        public Point2D(int X, int Y/*, int Z*/)
        {
            this.X = X;
            this.Y = Y;
            //this.Z = Z;
        }

        public Point2D(Point2D p1, Point2D p2)
        {
            this.X = p1.X + p2.X;
            this.Y = p1.Y + p2.Y;
           // this.Z = p1.Z + p2.Z;
        }

        public int GetDistanceSquared(Point2D point)
        {
            int dx = this.X - point.X;
            int dy = this.Y - point.Y;
           // int dz = this.Z - point.Z;
            return (dx * dx) + (dy * dy) /*+ (dz * dz)*/;            
        }

        public bool EqualsSS(Point2D p)
        {
            return p.X == this.X /*&& p.Z == this.Z */&& p.Y == this.Y;
        }

        public override int GetHashCode()
        {
            return (X + " " + Y /*+ " " + Z*/).GetHashCode();
        }

        public override string ToString()
        {
            return X + ", " + Y /*+ ", " + Z*/;
        }

        public static bool operator ==(Point2D one, Point2D two)
        {
            return one.EqualsSS(two);
        }

        public static bool operator !=(Point2D one, Point2D two)
        {
            return !one.EqualsSS(two);
        }

        public static Point2D operator +(Point2D one, Point2D two)
        {
            return new Point2D(one.X + two.X, one.Y + two.Y);
        }

        public static Point2D operator -(Point2D one, Point2D two)
        {
            return new Point2D(one.X - two.X, one.Y - two.Y);
        }

        public static Point2D Zero = new Point2D(0, 0);        
    }
}
