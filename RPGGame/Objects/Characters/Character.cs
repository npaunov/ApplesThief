using TeamAppleThief.Interfaces.Characters;

namespace TeamAppleThief.Objects.Characters
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using TeamAppleThief.Configuration;
    using TeamAppleThief.Enums;

    public abstract class Character : GameObject,ICharacter
    {

        protected Character()
        {
            this.AnimationFrame = 0;
            this.AnimationDelay = 10;
            this.State = State.Idle;
        }

        public virtual int AttackDamage { get; protected set; }

        public virtual int Defense { get; protected set; }

        public virtual int Speed { get; protected set; }

        public int Health { get; set; }

        [NotMapped]
        public int AttackRadius { get; protected set; }

        [NotMapped]
        public BoundingBox AttackBounds { get; protected set; }

        [NotMapped]
        public Direction Direction { get; set; }

        [NotMapped]
        public State State { get; set; }

        [NotMapped]
        public int AnimationFrame { get; set; }

        [NotMapped]
        public int AnimationDelay { get; set; }

        public bool Intersects(BoundingBox box)
        {
            return this.Bounds.Intersects(box);
        }

        public virtual void Update()
        {
            this.Bounds = new BoundingBox(new Vector3(this.Position.X, this.Position.Y, 0), new Vector3(this.Position.X + this.Width, this.Position.Y + this.Height, 0));

            if (this.Health <= 0)
            {
                this.IsAlive = false;
            }
        }

        public virtual void RespondToAttack(int damage)
        {
            this.Health -= damage - this.Defense;
        }
    }
}
