namespace Code.Player
{
    public class PlayerWeaponInitData
    {
        public float Damage { get; }

        public float Lifesteal { get; }

        public PlayerWeaponInitData(float damage, float lifesteal)
        {
            Damage = damage;
            Lifesteal = lifesteal;
        }
    }
}