using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        private Texture2D square;
        private Texture2D fighter;
        private Texture2D mud;
        private Texture2D fire;
        public Field[,] Fields { get; private set; }
        private int boardSize = 20;
        private int cellSize = 32;
        Field foundpath;
        AI ai;


        public BattleBoard(ContentManager content)
        {
            this.content = content;
            pixel = content.Load<Texture2D>("pixel");
            square = content.Load<Texture2D>("Square");
            fighter = content.Load<Texture2D>("fighter");
            fire = content.Load<Texture2D>("fire");
            mud = content.Load<Texture2D>("mud");
            Fields = new Field[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                {
                    Fields[i, j] = new Field(new Point(i,j));
                }

            Piece testPiece = new Piece(fighter,Fields[3,2],cellSize);
            testPiece.teamColor = Color.Red;
            testPiece.InsertOnBoard(this);

            Piece bluePiece = new Piece(fighter,Fields[8,7],cellSize);
            bluePiece.teamColor = Color.DarkBlue;
            bluePiece.InsertOnBoard(this);
            
            Fields[4, 3].Terrain = 1;
            Fields[2, 5].Terrain = 2;


            //Walls         
            Fields[3, 1].Terrain = -1;
            
            Fields[5, 1].Terrain = -1;
            Fields[5, 2].Terrain = -1;
            Fields[5, 3].Terrain = -1;
            Fields[5, 5].Terrain = -1;
            Fields[5, 6].Terrain = -1;
            Fields[5, 7].Terrain = -1;

            Fields[6, 12].Terrain = -1;
            Fields[7, 12].Terrain = -1;
            Fields[8, 12].Terrain = -1;
            Fields[9, 12].Terrain = -1;
            Fields[10, 12].Terrain = -1;
            Fields[11, 12].Terrain = -1;
            Fields[12, 12].Terrain = -1;


            Field target = Fields[14, 16];
            ai = new AI();
            foundpath = ai.FindPathToTarget(target, Fields[3, 2], this);
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach (var openNode in ai.openNodes)
            {
                spriteBatch.Draw(square, new Vector2(openNode.X * cellSize, openNode.Y * cellSize), Color.Green);
            }
            foreach (var openNode in ai.closedNodes)
            {
                spriteBatch.Draw(square, new Vector2(openNode.X * cellSize, openNode.Y * cellSize), Color.LightGreen);
            }
            for (int i = 0; i < boardSize; i++)
            { 
                for (int j = 0; j < boardSize; j++)
                {
                    switch (Fields[i,j].Terrain)
                    {
                        case -1:
                            spriteBatch.Draw(square,new Vector2(i*cellSize,j*cellSize),Color.Black);
                            break;
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
                        //foreach (var neighbor in Fields[i,j].Neighbors(this))
                        //{
                        //    spriteBatch.Draw(square,new Vector2(neighbor.X*cellSize,neighbor.Y*cellSize),Color.Yellow);
                        //}

                        
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
