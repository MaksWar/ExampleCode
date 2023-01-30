using System;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private EnemyHealth enemyHealth;

        private bool _isDead;
        
        public event Action OnDie;
        private void Start() =>
            enemyHealth.OnHealthIsOver += Die;

        private void OnDestroy() =>
            enemyHealth.OnHealthIsOver -= Die;

        private void Die()
        {
            _isDead = true;
            enemyMovement.enabled = false;
            Debug.Log("Die");
            OnDie?.Invoke();
            Destroy(gameObject);
        }
    }
}