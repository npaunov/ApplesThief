﻿namespace TeamAppleThief.Objects.Projectiles
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using TeamAppleThief.Enums;

    public class Firebolt : Projectile
    {
        #region Constants
        private const Name DefaultName = Name.Firebolt;
        private const int DefaultWidth = 60;
        private const int DefaultHeight = 30;
        private const int DefaultSpeed = 10;
        #endregion

        public Firebolt(int x, int y, Direction direction, int damage)
            : base(x, y, direction, damage)
        {
            this.Name = DefaultName;
            this.Width = DefaultWidth;
            this.Height = DefaultHeight;
            this.Speed = DefaultSpeed;
        }
    }
}
