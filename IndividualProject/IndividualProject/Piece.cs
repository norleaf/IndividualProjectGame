﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
        public int AttackDamage { get; set; }
        public int Armor { get; set; }
        public int ActionPoints { get; set; }
        public int MaxActionPoints { get; private set; }
        public int Health { get; set; }
        public Piece Target { get; set; }
        private BattleBoard battleBoard;
        private int steps;
        private const int STEPS = 25;
        public AI AI { get; set; }
        Vector2 dVector2;

        public Piece(Texture2D spriteTexture, Field field, int cellSize, int maxAP, bool clever, BattleBoard board) : base(spriteTexture, new Vector2(0,0))
        {
            this.battleBoard = board;
            this.Field = field;
            this.cellSize = cellSize;
            MaxActionPoints = maxAP;
            MoveVector = Vector2.Zero;
            if(clever) AI = new CleverAI(this);
            else AI = new AI(this);
        }

        public override void Update(GameTime gameTime)
        {
            //    Console.WriteLine("steps "+steps);
            if (steps > 0)
            {
                steps--;
                MoveVector += dVector2 / STEPS;
            }
            else
            {
                RemoveFromBoard();
                Field = battleBoard.Fields[MoveToField.X,MoveToField.Y];
                InsertOnBoard();
                MoveVector = Vector2.Zero;
                ActionPoints--;
                //call piece finished moving
                if (ActionPoints < 1)
                {
                    battleBoard.TurnComplete();
                }
                else
                {
                    if (Target != null)
                        TakeAction();
                    else
                        battleBoard.message = "Simulation Done";
                }
            }
            
        }

        public void TakeAction()
        {
            Console.WriteLine("Field: " + Field.X +","+ Field.Y);
            Console.WriteLine("Target Field: " + Target.Field.X + "," + Target.Field.Y);
            if (IsFieldAdjacent(Target.Field.X,Target.Field.Y))
            {
                Attack(Target);
            }
            else
            {
                MoveToField = AI.Path.Fields.Last();
                
                AI.Path.Fields.Remove(AI.Path.Fields.Last());
                steps = STEPS;
                Point dPoint = MoveToField.GridPoint - Field.GridPoint;
                dVector2 = new Vector2(dPoint.X * 32, dPoint.Y * 32);
            }
        }

        public void StartTurn()
        {
            //first pick a target, which also delivers the path to it.
            Target = AI.ChooseTarget(battleBoard);

            //take your first action
            if(Target!=null)
            TakeAction();
        }


        
        //public void StartMove()
        //{
        //    if (steps == 0)
        //    {
        //        Target = AI.ChooseTarget(battleBoard);
        //     //   AI.FindPathToTarget(Target, battleBoard);
        //        if (AI.Path.Fields.Count > 0)
        //        {
        //            //the last element of the list is the first step in our path
        //            MoveToField = AI.Path.Fields.Last();
                    

        //            //Are we moving to our targets position then attack instead!
        //            if (MoveToField.Equals(Target.Field))
        //            {
        //                Attack(Target);
        //                MoveToField = Field;
        //            }
        //            Point dPoint = MoveToField.GridPoint - Field.GridPoint;
        //            dVector2 = new Vector2(dPoint.X * 32, dPoint.Y * 32);

        //            //Only add steps to the animation if we are moving
        //            steps = STEPS;
        //        }
        //        else MoveToField = Field;
        //    }
        //}

        private void Attack(Piece Target)
        {
            battleBoard.SpawnBlood(Target.Field);
            Target.TakeDamage(AttackDamage);
            ActionPoints = 0;
        }

        private void TakeDamage(int attackDamage)
        {
            if (attackDamage > Armor)
            {
                Health = Health - (attackDamage - Armor);
            }
            if (Health <=0)
            {
                RemoveFromBoard();
                battleBoard.Pieces.Remove(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            
            spriteBatch.Draw(SpriteTexture, new Vector2(Field.X*cellSize,Field.Y*cellSize - cellSize)+ MoveVector + camera.Position, SourceRectangle, teamColor, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }

        public void RemoveFromBoard()
        {
            battleBoard.Fields[Field.X, Field.Y].Piece = null;
        }
        
        public void InsertOnBoard()
        {
            battleBoard.Fields[Field.X, Field.Y].Piece = this;
        }

        public virtual bool IsFieldAdjacent(int x, int y)
        {
            if (x >= Field.X - 1 && x <= Field.X + 1 && y >= Field.Y - 1 && y <= Field.Y + 1)
            {
                Console.WriteLine("Adjacent");
                return true;
                
            }
            Console.WriteLine("Not Adjacent");

            return false;
        }


        
    }
}
