using Game.Scripts.Game.Enemy;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int maxCountEnemy;
        [SerializeField] private float spawnRadius;
        [SerializeField] private EnemyShahedLogic enemy;

        private int _countEnemy;

        private void FixedUpdate()
        {
            SpawnEnemy();
            Draw.Circle(transform.position, spawnRadius, Color.cyan);
        }

        private void SpawnEnemy()
        {
            if (_countEnemy < maxCountEnemy)
            {
                var enemyShahed = Instantiate(enemy,
                    RandomPos(), Quaternion.identity, transform);

                enemyShahed.GetComponent<EnemyDeath>().OnDie += EnemyDie;
                _countEnemy++;
                
                Vector3 RandomPos()
                {
                    while (true)
                    {
                        var pos = Random.insideUnitSphere * spawnRadius + transform.position;

                        return pos;
                    }
                }
            }
        }

        private void EnemyDie() => 
            _countEnemy--;
    }
}