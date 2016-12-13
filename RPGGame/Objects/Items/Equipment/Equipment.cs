namespace TeamAppleThief.Objects.Items.Equipment
{
    using System.ComponentModel.DataAnnotations;

    using TeamAppleThief.Enums;
    using TeamAppleThief.Interfaces.Items;
    using TeamAppleThief.Objects.Characters.PlayableCharacters;

    public abstract class Equipment : Item, IEquipable
    {
        public Equipment(int speedBonus,int defenseBonus, int damageBonus, int healthBonus)
        {
            this.SpeedPointsBuff = speedBonus;
            this.DefensePointsBuff = defenseBonus;
            this.AttackPointsBuff = damageBonus;
            this.HealthPointsBuff = healthBonus;
        }

        [Required]
        public virtual Player Holder { get; set; }

        public EquipmentSlot Slot { get; set; }
    }
}
