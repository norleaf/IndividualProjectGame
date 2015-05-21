using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    
    public class MapFactory
    {
        private BattleBoard board;

        public MapFactory(BattleBoard board)
        {
            this.board = board;
        }

        public void Map1()
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
            board.Fields[12, 5].Terrain = -1;
            board.Fields[13, 4].Terrain = -1;
            board.Fields[13, 5].Terrain = -1;
            board.Fields[13, 6].Terrain = -1;
            board.Fields[13, 7].Terrain = -1;

            Piece redKnight = NewKnight(3, 1, Color.Red);
            Piece redFighter = NewFighter(1, 4, Color.Red);
            //Piece redFighter2 = NewFighter(2, 2, Color.Red);

            Piece blueKnight = NewKnight(13, 11, Color.Blue);
            Piece blueFighter = NewFighter(18, 14, Color.Blue);

            redKnight.Target = blueFighter;
            redFighter.Target = blueFighter;

            blueFighter.Target = redKnight;
            blueKnight.Target = redKnight;

        }

        private Piece NewKnight(int x, int y, Color teamColor)
        {
            Piece knight = new Piece(board.fighter, board.Fields[x,y], board.cellSize, 3, board );
            knight.Armor = 3;
            knight.AttackDamage = 4;
            knight.Health = 9;
            knight.teamColor = teamColor;
            board.Pieces.Enqueue(knight);
            knight.InsertOnBoard();
            return knight;
        }

        private Piece NewFighter(int x, int y, Color teamColor)
        {
            Piece fighter = new Piece(board.fighter, board.Fields[x, y], board.cellSize, 4, board);
            fighter.Armor = 2;
            fighter.AttackDamage = 5;
            fighter.Health = 7;
            fighter.teamColor = teamColor;
            board.Pieces.Enqueue(fighter);
            fighter.InsertOnBoard();
            return fighter;
        }
    }
}
