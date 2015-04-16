using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    public class Field
    {
        public Point Position { get; private set; }
        public Piece Visitor { get; set; }
        public Terrain Terrain { get; set; }

        public Field(int x, int y)
        {
            Position = new Point(x,y);
        }

        public bool IsOpen()
        {
            if (Visitor == null && Terrain.TT != Terrain.Type.wall)
            {
                return true;
            }
            return false;
        }

        public bool Enter(Piece piece)
        {
            if (IsOpen())
            {
                //remove reference to piece from old field
                piece.Field.Visitor = null;

                //Set a reference to the piece on this field
                Visitor = piece;

                //create reference to this field on piece
                piece.Field = this;

                //suffer terrain consequences
                Terrain.Effect(piece);
            }
        }
    }
}
