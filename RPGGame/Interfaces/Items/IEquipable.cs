namespace TeamAppleThief.Interfaces.Items
{
    using TeamAppleThief.Enums;

    public interface IEquipable
    {
        EquipmentSlot Slot { get; set; }
    }
}
