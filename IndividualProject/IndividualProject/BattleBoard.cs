using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IndividualProject
{
    public class BattleBoard
    {
        private ContentManager content;
        private Texture2D pixel;
        private Texture2D square;
        public Texture2D fighter;
        private Texture2D mud;
        private Texture2D fire;
        public Field[,] Fields { get; private set; }
        private int boardSize = 20;
        public readonly int cellSize = 32;
        List<Field> foundpath;
       // AI ai;
        private Piece activePiece;
        private bool _paused;
        private bool _justPressed;
        //private Piece targetPiece;
        public Queue<Piece> Pieces { get; private set; } 

        public BattleBoard(ContentManager content)
        {
            Pieces = new Queue<Piece>();
          //  ai = new AI();
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
            GenerateTerrain();

            activePiece = Pieces.Peek();

            ActionComplete();
        }

        private void GenerateTerrain()
        {
            MapFactory mf = new MapFactory();
            mf.Map1(this);
        }

        public void Update(GameTime gameTime)
        {
            
            CheckKeyBoard();
            
            if(!_paused) 
                activePiece.Update(gameTime);
        }

        private void CheckKeyBoard()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (_justPressed && !_paused) _paused = true;
                else if (_paused && _justPressed) _paused = false;
                _justPressed = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space))
                _justPressed = true;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach (var openNode in activePiece.AI.openNodes)
            {
                spriteBatch.Draw(square, new Vector2(openNode.X * cellSize, openNode.Y * cellSize), Color.Green);
            }
            foreach (var openNode in activePiece.AI.closedNodes)
            {
                spriteBatch.Draw(square, new Vector2(openNode.X * cellSize, openNode.Y * cellSize), Color.LightGreen);
            }
            for (int i = 0; i < boardSize; i++)
            {
                spriteBatch.Draw(pixel, new Vector2(0, i * cellSize), pixel.Bounds, Color.Black, 0f, new Vector2(0, 0), new Vector2(boardSize * cellSize, 1), SpriteEffects.None, 0f);
                spriteBatch.Draw(pixel, new Vector2(i * cellSize, 0), pixel.Bounds, Color.Black, 0f, new Vector2(0, 0), new Vector2(1, boardSize * cellSize), SpriteEffects.None, 0f);
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
                    //if (Fields[i,j].Piece!=null)
                    //{
                    //    Fields[i,j].Piece.Draw(spriteBatch,camera);
                    //}
                }
            }
            foreach (var piece in Pieces)
            {
                piece.Draw(spriteBatch,camera);
            }
            
            
        }

        public void ClearParents()
        {
            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                {
                    Fields[i, j].PathParent = null;
                }
        }


        public void ActionComplete()
        {
            if (activePiece.ActionPoints < 1)
            {
                activePiece = Pieces.Dequeue();
                Pieces.Enqueue(activePiece);
                activePiece.ActionPoints = activePiece.MaxActionPoints;
                
            }
            activePiece.StartMove();
            //Here add some checks to see if we can attack else move
            //List<Field> path = ai.FindPathToTarget(activePiece.Field, activePiece.Target.Field, this);
            //if (path.Count > 0)
            //{
            //    //the last element of the list is the first step in our path
            //    pieceMover.Destination = path.Last();
            //    pieceMover.StartMove();
            //}
            //else
            //{
            //    //Stay where you are instead of moving to field of last piece that moved
            //    pieceMover.Destination = activePiece.Field;
            //}
        }
    }
}
