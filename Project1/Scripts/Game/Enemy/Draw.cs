using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public static class Draw
    {
        public static void Circle(Vector3 center, float radius, Color color, float time = 0f)
        {
            Vector3 prevPos = center + new Vector3(radius, 0, 0);
            for (int i = 0; i < 30; i++)
            {
                float angle = (i + 1) / 30.0f * Mathf.PI * 2.0f;
                Vector3 newPos = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
                Debug.DrawLine(prevPos, newPos, color, time);
                prevPos = newPos;
            }
        }
    }
}