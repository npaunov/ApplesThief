﻿namespace TeamAppleThief.Objects.Projectiles
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using TeamAppleThief.Enums;

    public class Dagger : Projectile
    {
        #region Constants
        private const Name DefaultName = Name.Dagger;
        private const int DefaultWidth = 80;
        private const int DefaultHeight = 30;
        private const int DefaultSpeed = 10;
        #endregion

        public Dagger(int x, int y, Direction direction, int damage)
            : base(x, y, direction, damage)
        {
            this.Name = DefaultName;
            this.Width = DefaultWidth;
            this.Height = DefaultHeight;
            this.Speed = DefaultSpeed;
        }
    }
}
