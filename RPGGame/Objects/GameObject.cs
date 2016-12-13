namespace TeamAppleThief.Objects
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using TeamAppleThief.Enums;

    public abstract class GameObject
    {

        protected GameObject()
        {
            this.IsAlive = true;
        }

        public Name Name { get; set; }

        [NotMapped]
        public Texture2D Image { get; set; }

        [NotMapped]
        public virtual BoundingBox Bounds { get; set; }

        [NotMapped]
        public Vector2 Position { get; set; }

        [NotMapped]
        public int Width { get; set; }

        [NotMapped]
        public int Height { get; set; }

        [NotMapped]
        public bool IsAlive { get; set; }

        public void LoadImage(Texture2D image)
        {
            this.Image = image;
        }
    }
}
