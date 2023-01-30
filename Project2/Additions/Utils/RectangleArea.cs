using UnityEngine;

namespace Additions.Utils
{
    public class RectangleArea : MonoBehaviour
    {
        [SerializeField] private Color color = new Color(1, 0, 0); 
        
        public float Height = 1f;
        public float Width = 1f;
        
        public Rect Rectangle()
        {
            var rect = new Rect(0, 0, Width, Height);
            rect.center = transform.position;
            return rect;
        }
        
        private void OnDrawGizmos()
        {
            var vector3 = new Vector3(Width, Height, 0);
            Gizmos.color = color;
            Gizmos.DrawWireCube(transform.position, vector3);
        }
    }
}