using System;
using Code.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class PlayerHealthUi : MonoBehaviour
    {
        [SerializeField] private PlayerHealthChangedEffect _healthRegeneratedEffectPrefab;
        [SerializeField] private PlayerHealthChangedEffect _healthLostEffectPrefab;
        
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private Image _healthBarProgress;

        private void Awake()
        {
            _playerHealth.OnHealthUpdated += UpdateHealthBar;
            _playerHealth.OnDamageReceived += OnDamageReceived;
            _playerHealth.OnHealthRegenerated += OnHealthRegenerated;
        }

        private void OnDestroy()
        {
            _playerHealth.OnHealthUpdated -= UpdateHealthBar;
            _playerHealth.OnDamageReceived -= OnDamageReceived;
            _playerHealth.OnHealthRegenerated -= OnHealthRegenerated;
        }

        private void UpdateHealthBar()
        {
            var ratio = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
            _healthBarProgress.fillAmount = ratio;
        }
        
        private void OnDamageReceived(float value)
        {
            if(value <= 0)
                return;
            
            //TODO: pool
            var effect = Instantiate(_healthLostEffectPrefab, transform);
            effect.Init("-", value);
        }

        private void OnHealthRegenerated(float value)
        {
            if(value <= 0)
                return;
            
            //TODO: pool
            var effect = Instantiate(_healthRegeneratedEffectPrefab, transform);
            effect.Init("+",value);
        }
    }
}