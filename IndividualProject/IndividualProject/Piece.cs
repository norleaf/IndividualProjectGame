﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IndividualProject
{
    public class Piece : Sprite
    {
        public Point Field { get; set; }
        public Color teamColor { get; set; }
        private int cellSize;
        public int Attack { get; set; }
        public int Armor { get; set; }
        public int ActionPoints { get; set; }
        public int Health { get; set; }

        public Piece(Texture2D spriteTexture, Point field, int cellSize) : base(spriteTexture, new Vector2(0,0))
        {
            this.Field = field;
            this.cellSize = cellSize;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(SpriteTexture, new Vector2(Field.X*cellSize,Field.Y*cellSize) + camera.Position, SourceRectangle, teamColor, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }

        public virtual bool MoveToField(int x, int y, BattleBoard board)
        {
            if (IsFieldAdjacent(x, y))
            {
                
            }
            return false;
        }

        public virtual bool IsFieldAdjacent(int x, int y)
        {
            if (x >= Field.X - 1 && x <= Field.X + 1 && y >= Field.Y - 1 && y <= Field.Y + 1)
                return true;
            return false;
        }

        
    }
}