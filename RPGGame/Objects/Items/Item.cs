namespace TeamAppleThief.Objects.Items
{
    using System.ComponentModel.DataAnnotations;

    using TeamAppleThief.Interfaces.Items;
    using TeamAppleThief.Objects.Characters.PlayableCharacters;

    public abstract class Item : GameObject,IItem
    {
        public int Id { get; set; }

        [Required]
        public virtual Player Owner { get; set; }

        public int SpeedPointsBuff { get; set; }

        public int DefensePointsBuff { get; set; }

        public int AttackPointsBuff { get; set; }

        public int HealthPointsBuff { get; set; }
    }
}
