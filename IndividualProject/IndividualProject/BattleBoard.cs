using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace IndividualProject
{
    class BattleBoard
    {
        private ContentManager content;
        private Texture2D pixel;
        private Piece[,] pieces;
        private int[,] terrain;
        private int boardSize = 20;
        private int cellSize = 32;

        public BattleBoard(ContentManager content)
        {
            this.content = content;
            pixel = content.Load<Texture2D>("pixel");
            pieces = new Piece[boardSize,boardSize];
            terrain = new int[boardSize,boardSize];
            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                {
                    pieces[i, j] = null;
                    terrain[i, j] = 0;
                }
            Piece testPiece = new Piece(content.Load<Texture2D>("Square"),new Point(3,2),cellSize);
            testPiece.teamColor = Color.Red;
            pieces[testPiece.gridPosition.X, testPiece.gridPosition.Y] = testPiece;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (pieces[i,j]!=null)
                    {
                        pieces[i,j].Draw(spriteBatch,camera);
                    }
                    spriteBatch.Draw(pixel, new Vector2(0, j * cellSize), pixel.Bounds, Color.Black, 0f, new Vector2(0, 0), new Vector2(boardSize * cellSize, 1), SpriteEffects.None, 0f);

                }
                spriteBatch.Draw(pixel, new Vector2(i * cellSize, 0), pixel.Bounds, Color.Black, 0f, new Vector2(0, 0), new Vector2(1, boardSize * cellSize), SpriteEffects.None, 0f);
                
            }
            
        }


    }
}
