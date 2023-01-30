using Duckject.Core.Attributes;
using Game.Cooking.Core;
using Game.Cooking.Meals.Burgers;
using UnityEngine;

namespace Game.Cooking
{
	public class Plate : MonoBehaviour
	{
		[SerializeField] private Meals.Burgers.Burger burger;
		[SerializeField] private BurgerPlace burgerPlace;

		private IBurgerConstructor _burgerConstructor;

		[Quack]
		private void Construct(IBurgerConstructor burgerConstructor) =>
			_burgerConstructor = burgerConstructor;

		private void OnDestroy() =>
			_burgerConstructor.OnEndConstruct -= burgerPlace.TakeAPlace;

		private void Awake() =>
			_burgerConstructor.InitBurger(burger);

		private void Start() =>
			_burgerConstructor.OnEndConstruct += burgerPlace.TakeAPlace;

		public void ResetBurger()
		{
			burger.ResetBurger();
			burgerPlace.RemoveEqualMeal(burger);
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			var partOfBurger = col.GetComponent<PartOfBurger>();
			if (partOfBurger == null)
				return;

			if (_burgerConstructor.TryConstruct(partOfBurger) == false)
				return;

			_burgerConstructor.Construct(partOfBurger);
			partOfBurger.EndDrag();
		}
	}
}