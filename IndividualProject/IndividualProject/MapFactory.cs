using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    
    public class MapFactory
    {
        private BattleBoard _board;
        private bool cleverAI;
        public MapFactory(BattleBoard board)
        {
            _board = board;
            cleverAI = false;
        }

        public void Map1()
        {
            //mud
            _board.Fields[4, 3].Terrain = 1;
            _board.Fields[9, 9].Terrain = 1;
            _board.Fields[9, 7].Terrain = 1;
            _board.Fields[8, 7].Terrain = 1;
            _board.Fields[8, 8].Terrain = 1;
            _board.Fields[8, 9].Terrain = 1;
          //  _board.Fields[8, 10].Terrain = 1;
            _board.Fields[8, 11].Terrain = 1;
            _board.Fields[9, 11].Terrain = 1;

            _board.Fields[10, 13].Terrain = 1;
            _board.Fields[10, 14].Terrain = 1;
            _board.Fields[10, 15].Terrain = 1;
            _board.Fields[9, 13].Terrain = 1;
            _board.Fields[9, 14].Terrain = 1;
            _board.Fields[9, 15].Terrain = 1;
            _board.Fields[9, 16].Terrain = 1;
            _board.Fields[9, 17].Terrain = 1;
            _board.Fields[9, 18].Terrain = 1;

            //Fire
            _board.Fields[2, 5].Terrain = 2;

            //Walls         
            _board.Fields[3, 1].Terrain = -1;
            
            _board.Fields[5, 1].Terrain = -1;
            _board.Fields[5, 2].Terrain = -1;
            _board.Fields[5, 3].Terrain = -1;
            _board.Fields[5, 5].Terrain = -1;
            _board.Fields[5, 6].Terrain = -1;
            _board.Fields[5, 7].Terrain = -1;
            
            _board.Fields[9, 8].Terrain = -1;
          
            _board.Fields[6, 12].Terrain = -1;
            _board.Fields[7, 12].Terrain = -1;
            _board.Fields[8, 12].Terrain = -1;
            _board.Fields[9, 12].Terrain = -1;
            _board.Fields[10, 12].Terrain = -1;
            _board.Fields[11, 12].Terrain = -1;
            _board.Fields[12, 12].Terrain = -1;
            _board.Fields[12, 5].Terrain = -1;
            _board.Fields[13, 4].Terrain = -1;
            _board.Fields[13, 5].Terrain = -1;
            _board.Fields[13, 6].Terrain = -1;
            _board.Fields[13, 7].Terrain = -1;

            SetupPiecesPositions1();
           

            //redKnight.Target = blueFighter;
            //redFighter.Target = blueFighter;

            //blueFighter.Target = redKnight;
            //blueKnight.Target = redKnight;

        }

        private void SetupPiecesPositions1()
        {
            cleverAI = false;
            NewKnight(1, 4, Color.Red);
            NewSpearman(1, 14, Color.Red);
            NewAxeman(2, 7, Color.Red);
            NewSpearman(1, 1, Color.Red);

            cleverAI = true;
            NewKnight(12, 4, Color.Blue);
            NewSpearman(12, 14, Color.Blue);
            NewAxeman(12, 11, Color.Blue);
            NewSpearman(12, 7, Color.Blue);
        }

        private void SetupPiecesPositions2()
        {
            cleverAI = false;
            NewKnight(1, 3, Color.Red);
            NewSpearman(1,2, Color.Red);
            NewAxeman(2, 3, Color.Red);
            NewSpearman(2, 2, Color.Red);

            cleverAI = true;
            NewKnight(14, 15, Color.Blue);
            NewSpearman(14, 14, Color.Blue);
            NewAxeman(15, 15, Color.Blue);
            NewSpearman(15, 14, Color.Blue);
        }

        private Piece NewAxeman(int x, int y, Color teamColor)
        {
            Piece axeman = new Piece(_board.axeman, _board.Fields[x, y], _board.cellSize, 3, cleverAI, _board);
            axeman.Armor = 0;
            axeman.AttackDamage = 7;
            axeman.Health = 9;
            axeman.teamColor = teamColor;
            _board.Pieces.Add(axeman);
            axeman.InsertOnBoard();
            return axeman;
        }

        private Piece NewKnight(int x, int y, Color teamColor)
        {
            Piece knight = new Piece(_board.fighter, _board.Fields[x,y], _board.cellSize, 3, cleverAI, _board );
            knight.Armor = 3;
            knight.AttackDamage = 4;
            knight.Health = 9;
            knight.teamColor = teamColor;
            _board.Pieces.Add(knight);
            knight.InsertOnBoard();
            return knight;
        }

        private Piece NewSpearman(int x, int y, Color teamColor)
        {
            Piece fighter = new Piece(_board.spearman, _board.Fields[x, y], _board.cellSize, 4, cleverAI, _board);
            fighter.Armor = 2;
            fighter.AttackDamage = 5;
            fighter.Health = 7;
            fighter.teamColor = teamColor;
            _board.Pieces.Add(fighter);
            fighter.InsertOnBoard();
            return fighter;
        }
    }
}
