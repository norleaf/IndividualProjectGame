using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    public class MapFactory
    {

        public void Map1(BattleBoard board)
        {
            board.Fields[4, 3].Terrain = 1;
            board.Fields[2, 5].Terrain = 2;
            //Walls         
            board.Fields[3, 1].Terrain = -1;
            
            board.Fields[5, 1].Terrain = -1;
            board.Fields[5, 2].Terrain = -1;
            board.Fields[5, 3].Terrain = -1;
            board.Fields[5, 5].Terrain = -1;
            board.Fields[5, 6].Terrain = -1;
            board.Fields[5, 7].Terrain = -1;
            
            board.Fields[9, 8].Terrain = -1;
          
            board.Fields[6, 12].Terrain = -1;
            board.Fields[7, 12].Terrain = -1;
            board.Fields[8, 12].Terrain = -1;
            board.Fields[9, 12].Terrain = -1;
            board.Fields[10, 12].Terrain = -1;
            board.Fields[11, 12].Terrain = -1;
            board.Fields[12, 12].Terrain = -1;

            Piece testPiece = new Piece(board.fighter, board.Fields[2, 1], board.cellSize, 3, board);
            testPiece.teamColor = Color.Red;
            //testPiece.ActionPoints = 3;
            testPiece.InsertOnBoard(board);

            Piece bluePiece = new Piece(board.fighter, board.Fields[1, 16], board.cellSize, 3, board);
            bluePiece.teamColor = Color.DarkBlue;
            bluePiece.InsertOnBoard(board);

            Piece targetPiece = new Piece(board.fighter, board.Fields[17, 16], board.cellSize, 3, board);
            targetPiece.teamColor = Color.Yellow;
            targetPiece.InsertOnBoard(board);

            board.Pieces.Enqueue(testPiece);
            board.Pieces.Enqueue(bluePiece);
            board.Pieces.Enqueue(targetPiece);
            

            testPiece.Target = targetPiece;
            targetPiece.Target = bluePiece;
            bluePiece.Target = bluePiece;
        }
    }
}
