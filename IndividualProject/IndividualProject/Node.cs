using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    public class Node
    {
        public Node ParentNode { get; set; }
        public Point Field { get; set; }
        public int Cost { get; set; }

        public int Heuristic(int x, int y)
        {
            return (int) Math.Sqrt(Math.Pow(Math.Abs(x - Field.X),2) + Math.Pow(Math.Abs(y - Field.Y), 2));
        }
    }
}
