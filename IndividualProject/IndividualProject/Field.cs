using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    public class Field
    {
        public Point GridPoint { get; set; }
        public Piece Piece { get; set; }
        public int Terrain { get; set; }

        public Field(Point gridPoint)
        {
            GridPoint = gridPoint;
            Piece = null;
            Terrain = 0;
        }
    }
}
