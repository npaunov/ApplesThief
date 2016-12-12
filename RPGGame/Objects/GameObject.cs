﻿namespace TeamAndatHypori.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using TeamAndatHypori.Enums;

    public abstract class GameObject
    {

        protected GameObject()
        {
            this.IsAlive = true;
        }

        public Name Name { get; set; }

        public Texture2D Image { get; set; }

        public virtual BoundingBox Bounds { get; set; }

        public Vector2 Position { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool IsAlive { get; set; }

        public void LoadImage(Texture2D image)
        {
            this.Image = image;
        }
    }
}
