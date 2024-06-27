using System;
using UnityEngine;

namespace Code.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public event Action OnHealthUpdated;

        public event Action<float> OnHealthRegenerated; 
        public event Action<float> OnDamageReceived;
        
        
        public float CurrentHealth { get; private set; }
        public float MaxHealth => _initData?.Health ?? 0;
        public bool IsAlive => CurrentHealth > 0;
        
        private PlayerHealthInitData _initData;
        
        public void Init(PlayerHealthInitData initData)
        {
            _initData = initData;
            ResetHealth();
        }

        private void ResetHealth()
        {
            CurrentHealth = _initData.Health;
            OnHealthUpdated?.Invoke();
        }

        public void Heal(float value)
        {
            var newHealthValue = Mathf.Clamp(CurrentHealth + value, 0, MaxHealth);
            var healthDifference = newHealthValue - CurrentHealth;
            CurrentHealth = newHealthValue;
            OnHealthUpdated?.Invoke();
            OnHealthRegenerated?.Invoke(healthDifference);
        }

        public float ReceiveDamage(float damage)
        {
            var damageReduction = _initData.Armor / 100f;
            var finalDamage = damage * (1 - damageReduction);
            finalDamage = Mathf.Clamp(finalDamage, 0, damage);
            
            CurrentHealth = Mathf.Clamp(CurrentHealth - finalDamage, 0, MaxHealth);
            OnHealthUpdated?.Invoke();
            OnDamageReceived?.Invoke(finalDamage);

            return finalDamage;
        }
    }
}