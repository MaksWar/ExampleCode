using UnityEngine;

namespace Game.Enemy
{
    public class EnemyShahedLogic : MonoBehaviour
    {
        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private EnemyHealth enemyHealth;
        [SerializeField] private EnemyDeath enemyDeath;

        private void FixedUpdate()
        {
            if (enemyMovement.Status == EnemyStatus.PathCompleted)
                enemyHealth.TakeDamage(9999);
        }
    }
}