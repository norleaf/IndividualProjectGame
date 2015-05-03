using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    public class Camera
    {
        public Camera(Vector2 position, Vector2 screenSize)
        {
            Position = position;
            ScreenSize = screenSize;
        }

        public Vector2 Position { get; set; }
        public Vector2 ScreenSize { get; set; }
        public Rectangle ViewRectangle
        {
            get
            {
                return new Rectangle((int) Position.X,(int) Position.Y,(int) ScreenSize.X,(int) ScreenSize.Y);    
            }
        }

        public float Right { get { return Position.X + ScreenSize.X; } }
        public float Left { get { return Position.X; } }
        public float Top { get { return Position.Y; } }
        public float Bottom { get { return Position.Y + ScreenSize.Y; } }
    }
}
