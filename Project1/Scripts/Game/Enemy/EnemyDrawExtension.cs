using Game.Scripts.Game.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy
{
    public static class EnemyDrawExtension
    {
        public static void DrawPath(this NavMeshAgent agent)
        {
            if (agent.path.IsPosible())
                for (int i = 0; i < agent.path.corners.Length; i++)
                {
                    if (i + 1 < agent.path.corners.Length)
                    {
                        Draw.Circle(agent.path.corners[i + 1], 0.5f, Color.red);
                        Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.yellow);
                    }
                }
        }
    }
}