using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace IndividualProject
{
    public class BattleBoard
    {
        private ContentManager content;
        private Texture2D pixel;
        private Texture2D mud;
        private Texture2D fire;
        private Field[,] Fields;
        private int boardSize = 20;
        private int cellSize = 32;

        public BattleBoard(ContentManager content)
        {
            this.content = content;
            pixel = content.Load<Texture2D>("pixel");
            fire = content.Load<Texture2D>("fire");
            mud = content.Load<Texture2D>("mud");
            Fields = new Field[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                {
                    Fields[i, j] = new Field(new Point(i,j));
                }

            Piece testPiece = new Piece(content.Load<Texture2D>("Square"),new Point(3,2),cellSize);
            testPiece.teamColor = Color.Red;
            Fields[testPiece.Field.X, testPiece.Field.Y].Piece = testPiece;
            Fields[4, 3].Terrain = 1;
            Fields[2, 5].Terrain = 2;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    switch (Fields[i,j].Terrain)
                    {
                        case 1:
                            spriteBatch.Draw(mud,new Vector2(i*cellSize,j*cellSize));
                            break;
                        case 2:
                            spriteBatch.Draw(fire, new Vector2(i*cellSize,j*cellSize));
                            break;
                    }
                    if (Fields[i,j].Piece!=null)
                    {
                        Fields[i,j].Piece.Draw(spriteBatch,camera);
                    }
                }
            }
            for (int i = 0; i < boardSize; i++)
            {
                spriteBatch.Draw(pixel, new Vector2(0, i * cellSize), pixel.Bounds, Color.Black, 0f, new Vector2(0, 0), new Vector2(boardSize * cellSize, 1), SpriteEffects.None, 0f);
                spriteBatch.Draw(pixel, new Vector2(i * cellSize, 0), pixel.Bounds, Color.Black, 0f, new Vector2(0, 0), new Vector2(1, boardSize * cellSize), SpriteEffects.None, 0f);
            }
        }


        
    }
}
