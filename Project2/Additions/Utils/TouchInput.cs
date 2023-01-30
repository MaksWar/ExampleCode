using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Additions.Utils
{
    public class TouchInput : MonoBehaviour
    {
        public event Action OnClick;
        public event Action OnDragStart;
        public event Action OnDragFinish;

        [SerializeField] private bool interactable;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("sdfasdflkjasd;lfja;sljfd;lasjdf;lasjdf;lsjadf");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
        }
    }
}
