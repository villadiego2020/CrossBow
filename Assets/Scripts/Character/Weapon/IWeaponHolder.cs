namespace Win.Character
{
    public interface IWeaponHolder
    {
        bool EquipWeapon<T>(T weapon) where T : IWeapon;
    }
}
