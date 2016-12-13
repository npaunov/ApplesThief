namespace TeamAppleThief.Objects.Characters.PlayableCharacters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using TeamAppleThief.Configuration;
    using TeamAppleThief.Enums;
    using TeamAppleThief.Objects.Characters.NPCs.Enemies;
    using TeamAppleThief.Objects.Items;
    using TeamAppleThief.Objects.Items.Consumables;
    using TeamAppleThief.Objects.Items.Equipment;

    public delegate void OnDeathEventHandler(object sender, EventArgs args);

    public abstract class Player : Character
    {
        public event OnDeathEventHandler OnDeath;
        private int maxHealth;
        private int inventoryIsFullTimeout;

        protected Player()
        {
            this.ActivePotions = new List<Potion>();
            this.PlayerEquipment = new HashSet<Equipment>();
            this.Inventory = new List<Item>();
            this.Direction = Direction.Right;
        }

        public int Id { get; set; }

        [InverseProperty("Holder")]
        public virtual ICollection<Equipment> PlayerEquipment { get; set; }

        [InverseProperty("Drinker")]
        public virtual IList<Potion> ActivePotions { get; set; }

        [InverseProperty("Owner")]
        public virtual IList<Item> Inventory { get; set; }

        [NotMapped]
        public int Experience { get; set; }

        [NotMapped]
        public override int AttackDamage
        {
            get
            {
                int attackBonus = this.PlayerEquipment.Sum(item => item.AttackPointsBuff);
                attackBonus += this.ActivePotions.Sum(potion => potion.AttackPointsBuff);
                return base.AttackDamage + attackBonus;
            }
        }

        [NotMapped]
        public override int Defense
        {
            get
            {
                int defenseBonus = this.PlayerEquipment.Sum(item => item.DefensePointsBuff);
                defenseBonus += this.ActivePotions.Sum(potion => potion.DefensePointsBuff);
                return base.Defense + defenseBonus;
            }
        }

        [NotMapped]
        public override int Speed
        {
            get
            {
                int speedBonus = this.PlayerEquipment.Sum(item => item.SpeedPointsBuff);
                speedBonus += this.ActivePotions.Sum(potion => potion.SpeedPointsBuff);
                return base.Speed + speedBonus;
            }
        }

        [NotMapped]
        public int MaxHealth
        {
            get
            {
                int healthBonus = this.PlayerEquipment.Sum(item => item.HealthPointsBuff);
                healthBonus += this.ActivePotions.Sum(potion => potion.HealthPointsBuff);
                return this.maxHealth + healthBonus;
            }
            protected set { this.maxHealth = value; }
        }

        [NotMapped]
        public int Level { get; protected set; }

        [NotMapped]
        public bool InventoryIsFull
        {
            get
            {
                if (this.inventoryIsFullTimeout > 0)
                {
                    this.inventoryIsFullTimeout--;
                    return true;
                }

                return false;
            }
        }

        public override void Update()
        {
            if (this.Health <= 0)
            {
                if (this.OnDeath != null)
                {
                    this.OnDeath(this, new EventArgs());
                }
            }
            this.Position = new Vector2(
                MathHelper.Clamp(this.Position.X, -Config.OffsetX, Config.ScreenWidth - Config.OffsetX - this.Width),
                MathHelper.Clamp(this.Position.Y, 140f, Config.ScreenHeight - Config.OffsetY - this.Height - 135));
            this.Bounds = new BoundingBox(new Vector3(this.Position.X + Config.OffsetX, this.Position.Y + Config.OffsetY, 0), new Vector3(this.Position.X + Config.OffsetX + this.Width, this.Position.Y + Config.OffsetY + this.Height, 0));

            this.TimeOutPotions();
            this.UpdateAttackBounds();
        }

        public IList<Enemy> GetEnemiesInRange(IList<Enemy> enemies)
        {
            return enemies.Where(enemy => this.AttackBounds.Intersects(enemy.Bounds)).ToList();
        }

        public virtual void Attack(Enemy enemy)
        {
            enemy.RespondToAttack(this.AttackDamage);
        }

        public void AddExperience(Enemy enemy)
        {
            this.Experience += enemy.ExperienceReward;
        }

        public void DiscardItem(int inventoryIndex)
        {
            Item item = this.Inventory[inventoryIndex];
            this.Inventory[inventoryIndex] = null;
        }

        public void AddToInventory(Item item)
        {
            bool isAdded = false;
            for (int i = 0; i < Config.InventorySize; i++)
            {
                if (i == this.Inventory.Count)
                {
                    isAdded = true;
                    item.Owner = this;
                    this.Inventory.Add(item);
                    break;
                }
                else if (this.Inventory.ElementAt(i) == null)
                {
                    isAdded = true;
                    item.Owner = this;
                    this.Inventory[i] = item;
                    break;
                }
            }

            if (!isAdded)
            {
                this.inventoryIsFullTimeout = Config.InventoryIsFullMessageTimeout;
            }
        }

        public void UnequipItem(EquipmentSlot slot)
        {
            if (!this.IsInventoryFull() && this.PlayerEquipment.Any(x => x.Slot == slot))
            {
                Equipment itemToRemove = this.PlayerEquipment.FirstOrDefault(x => x.Slot == slot);
                this.AddToInventory(itemToRemove);
                this.PlayerEquipment.Remove(itemToRemove);
            }
            else
            {
                this.inventoryIsFullTimeout = Config.InventoryIsFullMessageTimeout;
            }
        }

        public void UseItem(int inventoryIndex)
        {
            if (this.Inventory.Count > inventoryIndex)
            {
                if (this.Inventory.ElementAt(inventoryIndex) is Potion)
                {
                    var potion = this.Inventory.ElementAt(inventoryIndex) as Potion;
                    if (potion is HealingPotion && this.Health < this.MaxHealth)
                    {
                        var newHealthPoints = this.Health + potion.HealthPointsBuff;
                        this.Health = newHealthPoints > this.MaxHealth ? this.MaxHealth : newHealthPoints;

                        this.Inventory[inventoryIndex] = null;
                    }
                    else if (potion is HealingPotion == false)
                    {
                        potion.Drinker = this;
                        this.ActivePotions.Add(potion);
                        this.Inventory[inventoryIndex] = null;
                    }
                }
                else if (this.Inventory.ElementAt(inventoryIndex) is Equipment)
                {
                    var equipment = this.Inventory[inventoryIndex] as Equipment;

                    if (!this.PlayerEquipment.Any(x => x.Slot == equipment.Slot))
                    {
                        equipment.Holder = this;
                        this.PlayerEquipment.Add(equipment);
                        this.Inventory[inventoryIndex] = null;
                    }
                }

            }
        }


        private bool IsInventoryFull()
        {
            return this.Inventory.Count == 5 && this.Inventory.All(x => x != null);
        }

        private void TimeOutPotions()
        {
            for (int i = 0; i < this.ActivePotions.Count; i++)
            {
                if (this.ActivePotions[i].Duration == 0)
                {
                    this.ActivePotions.RemoveAt(i);
                    i--;
                    continue;
                }
                this.ActivePotions[i].Duration--;
            }
        }

        private void UpdateAttackBounds()
        {
            if (this.Direction == Direction.Right)
            {
                this.AttackBounds = new BoundingBox(new Vector3(this.Position.X + Config.OffsetX + this.Width, this.Position.Y, 0), new Vector3(this.Position.X + Config.OffsetX + this.Width + this.AttackRadius, this.Position.Y + Config.OffsetY + this.Height, 0));
            }
            else if (this.Direction == Direction.Left)
            {
                this.AttackBounds = new BoundingBox(new Vector3(this.Position.X + Config.OffsetX - this.AttackRadius, this.Position.Y + Config.OffsetY - this.AttackRadius, 0), new Vector3(this.Position.X + Config.OffsetX, this.Position.Y + Config.OffsetY + this.Height, 0));
            }
        }
    }
}
