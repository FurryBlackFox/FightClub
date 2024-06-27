using System;
using UnityEngine;

namespace Code.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        public event Action OnAttack;
        
        [SerializeField] private PlayerHealth _playerHealth;

        private PlayerWeaponInitData _initData;
        
        public void Init(PlayerWeaponInitData initData)
        {
            _initData = initData;
        }

        public void Attack(Player enemy)
        {
            var dealtDamage = enemy.ReceiveDamage(_initData.Damage);

            var lifesteal = _initData.Lifesteal / 100f;
            var regeneratedHealth = dealtDamage * lifesteal;
            regeneratedHealth = Mathf.Clamp(regeneratedHealth, 0, dealtDamage);
            
            _playerHealth.Heal(regeneratedHealth);
            
            OnAttack?.Invoke();
        }
    }
}