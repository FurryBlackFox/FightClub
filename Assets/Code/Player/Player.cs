using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Utils;
using UnityEngine;

namespace Code.Player
{
    public class Player : MonoBehaviour
    {
        public event Action OnPlayerInitialized;

        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerWeapon _playerWeapon;
        
        public PlayerInitData InitData { get; private set; }
        
        public void Build(PlayerInitData initData)
        {
            InitData = initData;

            var weaponDamage = GetStatValue(Consts.StatsId.DamageID);
            var lifestealValue = GetStatValue(Consts.StatsId.LifeStealID);
            _playerWeapon.Init(new PlayerWeaponInitData(weaponDamage, lifestealValue));

            var healthValue = GetStatValue(Consts.StatsId.LifeID);
            var armorValue = GetStatValue(Consts.StatsId.ArmorID);
            _playerHealth.Init(new PlayerHealthInitData(healthValue, armorValue));
            
            OnPlayerInitialized?.Invoke();
        }

        public void AttackEnemy()
        {
            if(!_playerHealth.IsAlive)
                return;
            
            _playerWeapon.Attack(InitData.Enemy);
        }

        public float ReceiveDamage(float damage)
        {
            if(!_playerHealth.IsAlive)
                return 0;
            
            return _playerHealth.ReceiveDamage(damage);
        }

        private float GetStatValue(int id)
        {
            return InitData.StatRuntimeDataList.FirstOrDefault(s => s.ID == id)?.Value ?? 0;
        }
        
    }
}