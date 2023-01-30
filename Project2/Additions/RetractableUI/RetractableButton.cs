using System;
using UnityEngine;
using UnityEngine.UI;

namespace Additions.RetractableUI
{
    [RequireComponent(typeof(Button))]
    public class RetractableButton : RetractableBase
    {
        [SerializeField, HideInInspector] private Button button;
        
        public event Action OnClick;
        
        private void Start() => button.onClick.AddListener(() => OnClick?.Invoke());

        public void EnableInteractable() =>
            button.interactable = true;
        
        public void DisableInteractable() =>
            button.interactable = false;
        protected override void OnValidate()
        {
            base.OnValidate();
            button ??= GetComponent<Button>();
        }
    }
}