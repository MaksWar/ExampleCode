using System;
using Additions.Extensions;
using Game.Character;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHp;

        public float Current => _currentHealth >= 0 ? _currentHealth : 0;
        
        public float Max { get; }
        
        private float _currentHealth;
        
        public event Action<float> OnHealthChanged;
        public event Action OnHealthIsOver;

        private void Awake() => 
            _currentHealth = maxHp;

        public void TakeDamage(float health)
        {
            if (health.IsBelowOrEqualZero()) return;
            
            _currentHealth -= health;

            OnHealthChanged?.Invoke(_currentHealth);
            if (_currentHealth.IsBelowOrEqualZero())
                OnHealthIsOver?.Invoke();
        }

        public void TakeHeal(float health)
        {
            if (health.IsBelowOrEqualZero()) return;

            _currentHealth += health;

            OnHealthChanged?.Invoke(_currentHealth);
        }
    }
}