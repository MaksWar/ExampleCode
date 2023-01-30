using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Additions.DragNDrop
{
    [RequireComponent(typeof(EventTrigger))]
    public class DragNDrop2D : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        private static DragNDrop2D _draggedItem;

        [Header("Settings")] [SerializeField] private bool _interactable = true;
        [SerializeField] private bool backToStartPosition = true;
        [SerializeField] private bool autoMoveToTarget = true;
        [SerializeField] private float distanceForAutoMoveToTarget = 2.5f;
        [SerializeField] private Vector2 offset = Vector2.zero;
        [SerializeField] private bool needAutoGenerateStartPoint = true;
        [SerializeField] private bool needAutoGenerateTarget = true;

        [Space, Header("Components")] [SerializeField]
        private GameObject startPoint;

        [SerializeField] private GameObject target;

        [Space, Header("Movement Animation")] [SerializeField]
        private MovementAnimation movementAnimation;

        private Camera _mainCamera;
        private Coroutine _movementCoroutine;
        private bool _isMovementToDefault;

        public GameObject Target
        {
            get => target;
            set => target = value;
        }

        private bool IsDragged { get; set; }

        public GameObject StartPoint
        {
            get => startPoint;
            set => startPoint = value;
        }

        public event Action<DragNDrop2D> OnMovementToTargetCompleted;
        public event Action<DragNDrop2D> OnBeginDragCustom;
        public event Action<DragNDrop2D> OnEndDragCustom;
        public event Action<DragNDrop2D> OnPointerUpCustom;
        public event Action<DragNDrop2D> OnClick;

        private void OnValidate()
        {
            if (!_mainCamera) _mainCamera = Camera.main;
        }

        protected virtual void Awake()
        {
            if (!_mainCamera) _mainCamera = Camera.main;

            if (needAutoGenerateStartPoint && !startPoint) GenerateStartingPoint();

            if (needAutoGenerateTarget && !target) GenerateTarget();
        }

        private void Update()
        {
            if (_draggedItem != this || !IsDragged) return;

            var pos = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + (Vector3)offset;
            pos.z = transform.position.z;

            transform.position = pos;

            if (target != null && autoMoveToTarget && CheckDistanceToTarget()) MoveToTarget();
        }

        protected virtual void OnDisable() =>
            StopMovementCoroutine();

        public void GenerateStartingPoint()
        {
            if (startPoint != null)
                Destroy(startPoint.gameObject);

            startPoint = new GameObject($"{gameObject.name} Start Point");

            CheckAthor(startPoint);

            startPoint.transform.SetParent(transform.parent);
            startPoint.transform.localPosition = transform.localPosition;
        }

        public void GenerateTarget()
        {
            target = new GameObject($"{gameObject.name} Target");

            CheckAthor(target);

            target.transform.position = transform.position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartDrag();
        }

        public void StartDrag()
        {
            if (!_interactable || _draggedItem) return;

            IsDragged = true;

            OnBeginDragCustom?.Invoke(this);

            StopMovementCoroutine();

            _movementCoroutine = StartCoroutine(Movement(Movements.ToTouch));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            FinishDrag();
        }

        public void FinishDrag()
        {
            if (!IsDragged) return;

            EndDrag();

            if (backToStartPosition)
                _movementCoroutine = StartCoroutine(Movement(Movements.ToDefault));

            OnPointerUpCustom?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData eventData) => 
            OnClick?.Invoke(this);

        public void Interactable(bool interactable) =>
            _interactable = interactable;

        [ContextMenu("Move To Target")]
        public void MoveToTarget()
        {
            EndDrag();

            _movementCoroutine = StartCoroutine(Movement(Movements.ToTarget));
        }

        [ContextMenu("Move To Default")]
        public void MoveToDefault()
        {
            EndDrag();

            _movementCoroutine = StartCoroutine(Movement(Movements.ToDefault, false));
            transform.SetParent(startPoint.transform.parent.gameObject.transform);
        }

        private void CheckAthor(GameObject obj)
        {
            if (gameObject.TryGetComponent(out RectTransform rectTransform))
                obj.AddComponent<LayoutElement>().ignoreLayout = true;
        }

        private void EndDrag()
        {
            IsDragged = false;

            StopMovementCoroutine();
            _draggedItem = null;
        }

        private void StopMovementCoroutine()
        {
            if (_movementCoroutine != null)
                StopCoroutine(_movementCoroutine);
        }

        private bool CheckDistanceToTarget() =>
            Vector2.Distance(transform.position, target.transform.position) <=
            distanceForAutoMoveToTarget;

        private IEnumerator Movement(Movements movement, bool smoothMovement = true)
        {
            float t = 0;
            var initialPosition = transform.position;

            _isMovementToDefault = movement == Movements.ToDefault;

            while (t < 1)
            {
                t += smoothMovement ? Time.deltaTime / movementAnimation.Duration : 1;

                switch (movement)
                {
                    case Movements.ToTouch:
                        var newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + (Vector3)offset;
                        newPosition.z = transform.position.z;
                        transform.position = Vector3.Lerp(initialPosition, newPosition,
                            movementAnimation.Curve.Evaluate(t));
                        break;

                    case Movements.ToDefault:
                        transform.position = Vector3.Lerp(initialPosition, startPoint.transform.position,
                            movementAnimation.Curve.Evaluate(t));
                        break;

                    case Movements.ToTarget:
                        var position = target.transform.position;
                        transform.position = Vector3.Lerp(initialPosition, position,
                            movementAnimation.Curve.Evaluate(t));
                        break;
                }

                yield return null;
            }

            switch (movement)
            {
                case Movements.ToTouch:
                    _draggedItem = this;
                    break;

                case Movements.ToTarget:
                    OnMovementToTargetCompleted?.Invoke(this);
                    break;

                case Movements.ToDefault:
                    OnEndDragCustom?.Invoke(this);
                    break;
            }
        }

        [Serializable]
        private class MovementAnimation
        {
            [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

            [SerializeField] private float duration = 0.5f;

            public AnimationCurve Curve => curve;
            public float Duration => duration;
        }

        private enum Movements
        {
            ToTouch = 0,
            ToDefault = 1,
            ToTarget = 2
        }
    }
}