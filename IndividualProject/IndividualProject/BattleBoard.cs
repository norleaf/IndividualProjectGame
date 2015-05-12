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
        List<Field> foundpath;
        AI ai;
        private Piece activePiece;
        private Piece targetPiece;
        PieceMover pieceMover;
        private Queue<Piece> pieces; 

        public BattleBoard(ContentManager content)
        {
            pieces = new Queue<Piece>();
            ai = new AI();
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

            pieceMover = new PieceMover(this);

            Piece testPiece = new Piece(fighter,Fields[2,1],cellSize, 3);
            testPiece.teamColor = Color.Red;
            testPiece.ActionPoints = 3;
            testPiece.InsertOnBoard(this);

            activePiece = testPiece;
            

            Piece bluePiece = new Piece(fighter,Fields[1,16],cellSize, 3);
            bluePiece.teamColor = Color.DarkBlue;
            bluePiece.InsertOnBoard(this);

            targetPiece = new Piece(fighter,Fields[17,16],cellSize, 3);
            targetPiece.teamColor = Color.Yellow;
            targetPiece.InsertOnBoard(this);

            pieces.Enqueue(bluePiece);
            pieces.Enqueue(targetPiece);
            pieces.Enqueue(testPiece);

            testPiece.Target = targetPiece;
            targetPiece.Target = bluePiece;
            bluePiece.Target = bluePiece;

         //   Field target = Fields[14, 16];
            
          //  foundpath = ai.FindPathToTarget(Fields[3, 2], target, this);
            ActionComplete();
        }

        private void GenerateTerrain()
        {
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

            Fields[9, 8].Terrain = -1;

            Fields[6, 12].Terrain = -1;
            Fields[7, 12].Terrain = -1;
            Fields[8, 12].Terrain = -1;
            Fields[9, 12].Terrain = -1;
            Fields[10, 12].Terrain = -1;
            Fields[11, 12].Terrain = -1;
            Fields[12, 12].Terrain = -1;
        }

        public void Update(GameTime gameTime)
        {
            pieceMover.Update(gameTime, activePiece);
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
            foreach (var piece in pieces)
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
                activePiece = pieces.Dequeue();
                pieces.Enqueue(activePiece);
                activePiece.ActionPoints = activePiece.MaxActionPoints;
                
            }
            //Here add some checks to see if we can attack else move
            List<Field> path = ai.FindPathToTarget(activePiece.Field, activePiece.Target.Field, this);
            if (path.Count > 0)
            {
                //the last element of the list is the first step in our path
                pieceMover.Destination = path.Last();
                pieceMover.StartMove();
            }
            else
            {
                //Stay where you are instead of moving to field of last piece that moved
                pieceMover.Destination = activePiece.Field;
            }
        }
    }
}
