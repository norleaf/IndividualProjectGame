using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IndividualProject
{
    public class Piece : Sprite
    {
        public Field Field { get; set; }
        public Field MoveToField { get; set; }
        public Vector2 MoveVector { get; set; }
        public Color teamColor { get; set; }
        private int cellSize;
        public int Attack { get; set; }
        public int Armor { get; set; }
        public int ActionPoints { get; set; }
        public int MaxActionPoints { get; private set; }
        public int Health { get; set; }
        public Piece Target { get; set; }
        private BattleBoard battleBoard;
        private int steps;
        private const int STEPS = 20;
        public AI AI { get; set; }

        public Piece(Texture2D spriteTexture, Field field, int cellSize, int maxAP, BattleBoard board) : base(spriteTexture, new Vector2(0,0))
        {
            this.battleBoard = board;
            this.Field = field;
            this.cellSize = cellSize;
            MaxActionPoints = maxAP;
            MoveVector = Vector2.Zero;
            AI = new AI(this);
        }

        public override void Update(GameTime gameTime)
        {
            //    Console.WriteLine("steps "+steps);
            if (steps > 0)
            {
                steps--;
                Point dPoint = MoveToField.GridPoint - Field.GridPoint;
                Vector2 dVector2 = new Vector2(dPoint.X * 32, dPoint.Y * 32);
                MoveVector += dVector2 / STEPS;
            }
            else
            {
                RemoveFromBoard(battleBoard);
                Field = MoveToField;
                InsertOnBoard(battleBoard);
                MoveVector = Vector2.Zero;
                ActionPoints--;
                //call piece finished moving
                battleBoard.ActionComplete();
            }
        }

        public void StartMove()
        {
            if (steps == 0)
            {
                List<Field> path = AI.FindPathToTarget(Field, Target.Field, battleBoard);
                if (path.Count > 0)
                {
                    //the last element of the list is the first step in our path
                    MoveToField = path.Last();

                    //Only add steps to the animation if we are moving
                    steps = STEPS;
                }
                else MoveToField = Field;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            
            spriteBatch.Draw(SpriteTexture, new Vector2(Field.X*cellSize,Field.Y*cellSize - cellSize)+ MoveVector + camera.Position, SourceRectangle, teamColor, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }

        public void RemoveFromBoard(BattleBoard board)
        {
            board.Fields[Field.X, Field.Y].Piece = null;
        }
        
        public void InsertOnBoard(BattleBoard board)
        {
            board.Fields[Field.X, Field.Y].Piece = this;
        }

        public virtual bool IsFieldAdjacent(int x, int y)
        {
            if (x >= Field.GridPoint.X - 1 && x <= Field.GridPoint.X + 1 && y >= Field.GridPoint.Y - 1 && y <= Field.GridPoint.Y + 1)
                return true;
            return false;
        }

        
    }
}
