namespace TeamAppleThief.Objects.Items
{
    using System.ComponentModel.DataAnnotations;

    using TeamAppleThief.Interfaces.Items;
    using TeamAppleThief.Objects.Characters.PlayableCharacters;

    public abstract class Potion : Item, IConsumable
    {
        public int Duration { get; set; }

        [Required]
        public virtual Player Drinker { get; set; }
    }
}
