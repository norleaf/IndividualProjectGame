using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndividualProject
{
    public class Terrain
    {
        public Type TT { get; private set; }
        
        public enum Type
        {
            wall,
            clear,
            mud,
            fire
        }

        public Terrain(Type type)
        {
            TT = type;
        }

        public void Effect(Piece piece)
        {
            switch (TT)
            {
                    case Type.fire:
                    piece.TakeDamage(2);
                    break;
                    case Type.mud:
                    piece.SpendAction(1);
                    break;
            }
        }

        public int GetCost(Piece piece)
        {
            int cost = 1;
            switch (TT)
            {
                case Type.mud:
                    cost = 2;
                    break;
                case Type.fire:
                    if (piece.Health > 2)
                    {
                        cost = 4;
                    }
                    else
                    {
                        cost = -1;
                    }
                    break;
                case Type.wall:
                    cost = -1;
                    break;
            }
            return cost;
        }
    }
}
