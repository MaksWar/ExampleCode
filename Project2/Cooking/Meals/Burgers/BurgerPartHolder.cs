using Game.Cooking.Core;
using Game.Cooking.Meals.Data;
using UnityEngine;

namespace Game.Cooking.Meals.Burgers
{
	[RequireComponent(typeof(Collider2D))]
	public class BurgerPartHolder : MonoBehaviour, IInputControl
	{
		[SerializeField] private TypeOfPart typeOfPart;
		[SerializeField] private PartOfBurger partOfBurger;

		private PartOfBurger _part;

		private bool _isLock;

		public TypeOfPart TypeOfPart => typeOfPart;

		public void Lock() =>
			_isLock = true;

		public void UnLock() =>
			_isLock = false;

		private void OnMouseDown()
		{
			if(_isLock) return;

			_part = Instantiate(partOfBurger);

			_part.transform.position = transform.position;
			_part.StartDrag(gameObject);
		}

		private void OnMouseUp()
		{
			if(_isLock) return;

			_part.EndDrag(true);
		}
	}
}