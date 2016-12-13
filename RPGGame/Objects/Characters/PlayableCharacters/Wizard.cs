﻿namespace TeamAppleThief.Objects.Characters.PlayableCharacters
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using TeamAppleThief.Configuration;
    using TeamAppleThief.Enums;
    using TeamAppleThief.Interfaces.Characters;
    using TeamAppleThief.Objects.Characters.NPCs.Enemies;
    using TeamAppleThief.Objects.Projectiles;

    public class Wizard : Player, IProjectileProducable
    {
        #region Constants
        private const Name DefaultName = Name.Wizard;
        private const int DefaultAttackPoints = 25;
        private const int DefaultDefencePoints = 5;
        private const int DefaultSpeedPoints = 4;
        private const int DefaultHealthPoints = 100;
        private const int DefaultAttackRadius = 60;
        private const int DefaultLevel = 1;
        private const int DefaultExperience = 0;
        private const int DefaultWidth = 80;
        private const int DefaultHeight = 150;
        #endregion

        public Wizard() : this(0,0)
        {
        }

        public Wizard(int x, int y)
        {
            this.Name = DefaultName;
            this.AttackDamage = DefaultAttackPoints;
            this.Defense = DefaultDefencePoints;
            this.Speed = DefaultSpeedPoints;
            this.Health = DefaultHealthPoints;
            this.Level = DefaultLevel;
            this.Experience = DefaultExperience;
            this.Width = DefaultWidth;
            this.Height = DefaultHeight;
            this.Position = new Vector2(x, y);
            this.Bounds = new BoundingBox(new Vector3(x + Config.OffsetX, y + Config.OffsetY, 0), new Vector3(x + Config.OffsetX + this.Width, y + Config.OffsetY + this.Height, 0));
            this.AttackRadius = DefaultAttackRadius;
            this.AttackBounds = new BoundingBox(new Vector3(this.Position.X + Config.OffsetX + this.Width, this.Position.Y, 0), new Vector3(this.Position.X + Config.OffsetX + this.Width + this.AttackRadius, this.Position.Y + Config.OffsetY + this.Height, 0));
            this.MaxHealth = DefaultHealthPoints;
        }

        public override void RespondToAttack(int damage)
        {
            this.Health -= (damage - this.Defense) > 0 ? (damage - this.Defense) : 0;
        }

        public Projectile ProduceProjectile()
        {
            if (this.Direction == Direction.Right)
            {
                Projectile attack = new Firebolt((int)this.Position.X + this.Width + Config.OffsetX, (int)(this.Position.Y + this.Height), this.Direction, this.AttackDamage);
                return attack;
            }
            else
            {
                Projectile attack = new Firebolt((int)this.Position.X, (int)(this.Position.Y + this.Height), this.Direction, this.AttackDamage);
                return attack;
            }
        }

        public virtual Projectile SpecialAttack()
        {
            if (this.Direction == Direction.Right)
            {
                Projectile attack = new Fireball((int)this.Position.X + this.Width + Config.OffsetX, (int)(this.Position.Y + this.Height), this.Direction, this.AttackDamage*2);
                return attack;
            }
            else
            {
                Projectile attack = new Fireball((int)this.Position.X, (int)(this.Position.Y + this.Height), this.Direction, this.AttackDamage*2);
                return attack;
            }
        }
    }
}
