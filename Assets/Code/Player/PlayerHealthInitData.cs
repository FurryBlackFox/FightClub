namespace Code.Player
{
    public class PlayerHealthInitData
    {
        public float Health { get; }

        public float Armor { get; }

        public PlayerHealthInitData(float health, float armor)
        {
            Health = health;
            Armor = armor;
        }
    }
}