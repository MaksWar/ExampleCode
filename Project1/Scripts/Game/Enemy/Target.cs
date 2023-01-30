using UnityEngine;

namespace Game.Enemy
{
    public class Target : MonoBehaviour
    {
        public RelatingTarget Relating;
        public float Radius;
        public int Priority;
    }
    
    public enum RelatingTarget 
    {
        Enemy = 0,
        Neturel = 1,
        Ally = 3,
    }
}