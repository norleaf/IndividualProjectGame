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
        private SpriteFont Font;
        public String message = "";
        private Texture2D pixel;
        private Texture2D square;
        public Texture2D fighter;
        private Texture2D mud;
        private Texture2D stone;
        private Texture2D bloodsplat;
        private Texture2D fire;
        private Texture2D bg;
        public Field[,] Fields { get; private set; }
        private int boardSize = 20;
        public readonly int cellSize = 32;
       //List<Field> foundpath;
       // AI ai;
        private List<Sprite> sprites; 
        private Piece activePiece;
        private bool _paused;
        private bool _justPressed;
        //private Piece targetPiece;
        public List<Piece> Pieces { get; private set; } 

        public BattleBoard(ContentManager content)
        {
            Pieces = new List<Piece>();
          //  ai = new AI();
            this.content = content;
            Font = content.Load<SpriteFont>("superfont");
            bg = content.Load<Texture2D>("bg");
            pixel = content.Load<Texture2D>("pixel");
            square = content.Load<Texture2D>("Square");
            fighter = content.Load<Texture2D>("fighter2");
            fire = content.Load<Texture2D>("fire");
            mud = content.Load<Texture2D>("mud");
            stone = content.Load<Texture2D>("stone");
            bloodsplat = content.Load<Texture2D>("bloodsplat");
            sprites = new List<Sprite>();
            Fields = new Field[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                {
                    Fields[i, j] = new Field(new Point(i,j));
                }
            GenerateTerrain();

         //   activePiece = Pieces[0];

            TurnComplete();
        }

        private void GenerateTerrain()
        {
            MapFactory mf = new MapFactory(this);
            mf.Map1();
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
            spriteBatch.Draw(bg,new Vector2(0,0));
            foreach (var openNode in activePiece.AI.OpenNodes)
            {
                spriteBatch.Draw(square, new Vector2(openNode.X * cellSize, openNode.Y * cellSize), Color.Green);
            }
            foreach (var closedNode in activePiece.AI.ClosedNodes)
            {
                spriteBatch.Draw(square, new Vector2(closedNode.X * cellSize, closedNode.Y * cellSize), Color.LightGreen);
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
                            spriteBatch.Draw(stone,new Vector2(i*cellSize,j*cellSize));
                            break;
                        case 1:
                            spriteBatch.Draw(mud,new Vector2(i*cellSize,j*cellSize));
                            break;
                        case 2:
                            spriteBatch.Draw(fire, new Vector2(i*cellSize,j*cellSize));
                            break;
                    }
                }
            }
            foreach (var sprite in sprites)
            {
                sprite.Draw(spriteBatch, camera);
            }
            foreach (var piece in Pieces)
            {
                piece.Draw(spriteBatch,camera);
            }
            spriteBatch.DrawString(Font, message, Vector2.Zero, Color.Black);
            
            
        }

        public void ClearParents()
        {
            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                {
                    Fields[i, j].PathParent = null;
                }
        }


        public void TurnComplete()
        {
            activePiece = Pieces[0];
            Pieces.Remove(activePiece);
            Pieces.Add(activePiece);
            activePiece.ActionPoints = activePiece.MaxActionPoints;
            activePiece.StartTurn();
        }

       // public void ActionComplete()
       // {
       //     if (activePiece.ActionPoints < 1)
       //     {
       //         activePiece = Pieces[0];
       //         Pieces.Remove(activePiece);
       //         Pieces.Add(activePiece);
       //         activePiece.ActionPoints = activePiece.MaxActionPoints;
                
       //     }
       //     activePiece.StartMove();
       //}

        public void SpawnBlood(Field field)
        {
            Sprite blood = new Sprite(bloodsplat, new Vector2(field.X*cellSize,field.Y*cellSize));
            sprites.Add(blood);
        }
    }
}
