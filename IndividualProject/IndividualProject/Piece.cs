using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IndividualProject
{
    class Piece : Sprite
    {
        public Point gridPosition { get; set; }
        public Color teamColor { get; set; }
        private int cellSize;

        public Piece(Texture2D spriteTexture, Point gridPosition, int cellSize) : base(spriteTexture, new Vector2(0,0))
        {
            this.gridPosition = gridPosition;
            this.cellSize = cellSize;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(SpriteTexture, new Vector2(gridPosition.X*cellSize,gridPosition.Y*cellSize) + camera.Position, SourceRectangle, teamColor, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
