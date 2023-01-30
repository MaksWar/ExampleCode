using System;
using Additions.DragNDrop;
using Additions.Effects;
using DG.Tweening;
using Game.Cooking.Meals.Data;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace Game.Cooking.Meals.Burgers
{
	[RequireComponent(typeof(DragNDrop2D))]
	[RequireComponent(typeof(SortingGroup))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(JellyBounce))]
	[RequireComponent(typeof(Collider2D))]
	public class PartOfBurger : MonoBehaviour
	{
		[Header("TypeOfPart")]
		[SerializeField] private TypeOfPart typeOfPart;
		[Header("Components"), Space(15)]
		[SerializeField] private DragNDrop2D dragNDrop2D;
		[SerializeField] private SortingGroup sortingGroup;
		[SerializeField] private Rigidbody2D rb2D;
		[SerializeField] private JellyBounce jellyBounce;
		[SerializeField] private Collider2D coll2D;

		private bool _isDragging;

		private const int dragSortingOrder = 100;

		public event Action<PartOfBurger> OnSelected;

		public int SortingOrder => sortingGroup.sortingOrder;

		public TypeOfPart Type => typeOfPart;

		public void StartDrag(GameObject defaultPos)
		{
			if (_isDragging) return;

			_isDragging = true;

			dragNDrop2D.StartPoint = defaultPos;
			sortingGroup.sortingOrder = dragSortingOrder;

			dragNDrop2D.StartDrag();

			OnSelected?.Invoke(this);
		}

		public void EndDrag(bool useGravity = false)
		{
			if (_isDragging == false) return;

			_isDragging = false;

			if (useGravity)
			{
				rb2D.constraints = RigidbodyConstraints2D.None;
				rb2D.AddForce(new Vector2(Random.Range(-150, 150), Random.Range(50, 150)));
				coll2D.enabled = false;

				//TODO Пересмотреть удаление обьекта
				transform
					.DOScale(0f, 1.5f)
					.SetEase(Ease.InOutElastic)
					.OnComplete(() => Destroy(gameObject));
			}

			dragNDrop2D.Interactable(false);
			dragNDrop2D.FinishDrag();
		}

		public void BounceAnimation() =>
			jellyBounce.StartEffect();

		public void SetSortingOrder(int sortingOrder) =>
			sortingGroup.sortingOrder = sortingOrder;

		#region Editor

		private void OnValidate()
		{
			dragNDrop2D ??= GetComponent<DragNDrop2D>();
			sortingGroup ??= GetComponent<SortingGroup>();
			jellyBounce ??= GetComponent<JellyBounce>();
			rb2D ??= GetComponent<Rigidbody2D>();
			coll2D ??= GetComponent<Collider2D>();
		}

		#endregion
	}
}