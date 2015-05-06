using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    public class Field
    {
        public Field PathParent { get; set; }
        public float PathCost { get; set; }
        public Point GridPoint { get; set; }
        public Piece Piece { get; set; }
        public int Terrain { get; set; }
        public const int CLEAR = 0;
        public const int MUD = 1;
        public const int FIRE = 2;

        public int X
        {
            get { return GridPoint.X; }
        }

        public int Y
        {
            get { return GridPoint.Y; }
        }

        public float StepCost(Field steppingFromField)
        {
            if (steppingFromField.X == X || steppingFromField.Y == Y)
            {
                switch (Terrain)
                {
                    case CLEAR:
                        return 1;
                    case MUD:
                        return 3;
                    case FIRE:
                        return 4;
                    default:
                        return 1;
                }
            }
            else
            {
                switch (Terrain)
                {
                    case CLEAR:
                        return 1.44f;
                    case MUD:
                        return 4.24f;
                    case FIRE:
                        return 5.65f;
                    default:
                        return 1;
                }
            }
            
            //switch (Terrain)
            //    {
            //        case 0:
            //            return 1;
            //        case 1:
            //            return 3;
            //        case 2:
            //            return 1;
            //        default:
            //            return 1;
            //    }
            
        }

        public Field(Point gridPoint)
        {
            PathParent = null;
            GridPoint = gridPoint;
            Piece = null;
            Terrain = 0;
        }

        public int Heuristic(int x, int y)
        {
            return (int) (1.35 * Math.Sqrt(Math.Pow(Math.Abs(x - GridPoint.X), 2) + Math.Pow(Math.Abs(y - GridPoint.Y), 2)));
           // return (x > y) ? x : y;
        }

        public float F(Field origin)
        {
            return Heuristic(origin.GridPoint.X, origin.GridPoint.Y) + PathCost;
        }

        public List<Field> Neighbors(BattleBoard board, Field target)
        {
            List<Field> neighbors = new List<Field>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (X+i > 0 && X+i < 20 && Y+j > 0 && Y+j < 20 && (i!=0 || j!=0))
                    {
                        if (board.Fields[X+i,Y+j].Terrain !=-1 && board.Fields[X+i,Y+j].Piece == null || (target.X == X+i && target.Y == Y+j))
                        {
                            neighbors.Add(board.Fields[X + i, Y + j]);
                        }
                        
                    }
                }
            }
            return neighbors;
        }
    }
}
