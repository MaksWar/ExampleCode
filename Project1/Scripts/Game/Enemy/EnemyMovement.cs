using System.Collections;
using System.Linq;
using Game.Scripts.Game.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyStatus status;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float agrroRadius = 15;

        private Target _target;
        private bool _pathCompleted;
        public EnemyStatus Status => status;


        private void Start() =>
            StartCoroutine(FindTarget());

        private void Update()
        {
            agent.DrawPath();
                //Draw.Circle(transform.position, agrroRadius, Color.red);
        }

        private Target ScanArea()
        {
            var target = transform.position.CheckAreaForTarget(agrroRadius, 5);
            
            return target.Count >= 1 ? target.First() : null;
        }

        private void Move()
        {
            if (_target && agent.HavePath(_target.transform.position)) 
                agent.destination = _target.transform.position;
        }

        private void PathCompleted()
        {
            status = EnemyStatus.PathCompleted;
            _target = null;
        }
        
        private IEnumerator FindTarget()
        {
            while (!_target)
            {
                status = EnemyStatus.FindTarget;
                yield return new WaitForSeconds(0.5f);

                if (ScanArea())
                {
                    _target = ScanArea();
                    StartCoroutine(MoveToTarget());
                }
            }
        }

        private IEnumerator MoveToTarget()
        {
            while (!_pathCompleted)
            {
                status = EnemyStatus.MoveToTarget;
                
                yield return new WaitForSeconds(0.1f);
                Move();
                
                yield return new WaitForSeconds(0.1f);
                _pathCompleted = agent.PathCompleted();

                if (_pathCompleted)
                    PathCompleted();
            }
        }
    }

    public enum EnemyStatus
    {
        FindTarget = 0,
        MoveToTarget = 1,
        PathCompleted = 2,
    }
}
