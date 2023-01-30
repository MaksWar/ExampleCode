using System;
using DG.Tweening;
using UnityEngine;

namespace Additions.RetractableUI
{
    [RequireComponent(typeof(RectTransform))]
    public class RetractableBase : MonoBehaviour
    {
        [SerializeField, HideInInspector] private RectTransform rectTransform;
        
        [SerializeField] private Vector2 showPos;
        [SerializeField] private Vector2 hidePos;
        public object Assembly { get; set; }

        public virtual void Show(float time = 0.8f, float scale = 1, Action completed = null)
        {
            rectTransform.DOAnchorPos(showPos, time).SetEase(Ease.OutBack);
            rectTransform.transform.DOScale(scale, time).SetEase(Ease.OutBack).OnComplete(() => completed());
        }

        public virtual void Hide(float time = 0.8f, float scale = 1, Action completed = null)
        {
            rectTransform.DOAnchorPos(hidePos, time).SetEase(Ease.InBack);
            rectTransform.transform.DOScale(scale, time).SetEase(Ease.InBack).OnComplete(() => completed());
        }
        
        [ContextMenu("CopyShowPosition")]
        public void CopyShowPosition() => 
            showPos = rectTransform.anchoredPosition;

        [ContextMenu("CopyHiddenPosition")]
        public void CopyHiddenPosition() => 
            hidePos = rectTransform.anchoredPosition;

        protected virtual void OnValidate() => 
            rectTransform ??= GetComponent<RectTransform>();
    }
}
