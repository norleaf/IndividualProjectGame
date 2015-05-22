using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    public class PieceMover
    {
      //  public Piece piece { get; set; }
        public Field Destination { get; set; }
        private BattleBoard battleBoard;
        private int steps;
        private const int STEPS = 45;

        public PieceMover(BattleBoard battleBoard)
        {
            this.battleBoard = battleBoard;
        }

        public void Update(GameTime gameTime, Piece piece)
        {
        //    Console.WriteLine("steps "+steps);
            if (steps > 0)
            {
                steps--;
                Point dPoint = Destination.GridPoint - piece.Field.GridPoint;
                Vector2 dVector2 = new Vector2(dPoint.X*32, dPoint.Y*32);
                piece.MoveVector += dVector2/STEPS;
            }
            else
            {
                piece.RemoveFromBoard();
                piece.Field = Destination;
                piece.InsertOnBoard();
                piece.MoveVector = Vector2.Zero;
                piece.ActionPoints--;
                //call piece finished moving
             //   battleBoard.ActionComplete();
            }
        }

        public void StartMove()
        {
            steps += STEPS;
        }
        
        public bool MovePiece(Piece piece, Field destination)
        {
            while (!piece.Field.Equals(destination))
            {
                
            }
            return true;
        }
    }
}
