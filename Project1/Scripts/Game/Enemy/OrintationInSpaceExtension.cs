using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy
{
    public static class OrintationInSpaceExtension
    {
        
        public static List<Target> CheckAreaForTarget(this Vector3 center, float radius = 10, int maxTarget = 1)
        {
            List<Target> targets = new List<Target>();
            Collider[] coliders = new Collider[maxTarget];
            int layerMask = 1 << LayerMask.NameToLayer("Entity");
            
            Physics.OverlapSphereNonAlloc(center, radius, coliders, layerMask);

            CheckColliders(coliders, targets);
            
            return targets;
        }
        
        private static void CheckColliders(Collider[] coliders, List<Target> targets)
        {
            foreach (var colider in coliders)
            {
                if (colider != null && colider.TryGetComponent(out Target target))
                    targets.Add(target);
            }
        }
        
        public static bool IsPosible(this NavMeshPath path) => //Нейминг метода
            path.status == NavMeshPathStatus.PathComplete;
        
        public static bool PathCompleted(this NavMeshAgent agent, float accuracy = 1f) => 
            !(Vector3.Distance(agent.transform.position, agent.path.corners.Last()) > accuracy);

        public static bool HavePath(this NavMeshAgent agent, Vector3 target) //Что бы работало нужно задать движение агенту
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(target, path);

            return path.status == NavMeshPathStatus.PathComplete;
        }
    }
}