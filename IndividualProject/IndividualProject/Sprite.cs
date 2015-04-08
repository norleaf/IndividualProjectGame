using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IndividualProject
{
    public class Sprite
    {
        public string Name { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Scale { get; set; }
        public Texture2D SpriteTexture { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Vector2 Position { get; set; }

        public Sprite(Texture2D spriteTexture, Vector2 position)
        {

            SpriteTexture = spriteTexture;
            Position = position;
            Origin = new Vector2(Position.X,Position.Y);
            Scale = new Vector2(1, 1);
            Velocity = new Vector2();
            SourceRectangle = new Rectangle((int)Position.X,(int)Position.Y,SpriteTexture.Width,SpriteTexture.Height);
        }

        public virtual Vector2 Center
        {
            get { return new Vector2(BoundingBox.Center.X, BoundingBox.Center.Y); }
        }

        public virtual float Rotation { get; set; }

        public Vector2 NextPosition { get; set; }

        public void MoveHorizontally(float distance)
        {
            Position = Vector2.Add(Position, new Vector2(distance, 0));
        }

        public void MoveVertically(float distance)
        {
            Position = Vector2.Add(Position, new Vector2(0, distance));
        }

        public virtual Vector2 Origin { get; set; }

        public virtual SpriteEffects SpriteEffect { get; set; }

        public static Vector2 Shake { get; set; }

        public virtual Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int) Position.X,(int) Position.Y,(int) (SourceRectangle.Width*Scale.X),(int) (SourceRectangle.Height*Scale.Y));
            }
        }

        public virtual void Die()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            // sprite logic goes here...
        }

        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(SpriteTexture, Position + camera.Position, SourceRectangle, Color.White, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }

        public virtual Sprite CloneAt(float x, float y)
        {
            return new Sprite(SpriteTexture, new Vector2(x, y));
        }

        public virtual Sprite CloneAt(float x)
        {
            return new Sprite(SpriteTexture, new Vector2(x, Position.Y));
        }

    }
}
