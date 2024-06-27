using System;
using Code.Utils;
using UnityEditor.Animations;
using UnityEngine;

namespace Code.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerWeapon _playerWeapon;
        
        private readonly int AttackAnimatorHash = Animator.StringToHash(Consts.AnimatorParams.Attack);
        private readonly int HealthAnimatorHash = Animator.StringToHash(Consts.AnimatorParams.Health);

        private void Awake()
        {
            _playerHealth.OnHealthUpdated += OnHealthUpdated;
            _playerWeapon.OnAttack += OnAttack;
        }

        private void OnDestroy()
        {
            _playerHealth.OnHealthUpdated -= OnHealthUpdated;
            _playerWeapon.OnAttack -= OnAttack;
        }

        private void OnHealthUpdated()
        {
            _animator.SetFloat(HealthAnimatorHash, _playerHealth.CurrentHealth);
        }

        private void OnAttack()
        {
            _animator.SetTrigger(AttackAnimatorHash);
        }
    }
}
