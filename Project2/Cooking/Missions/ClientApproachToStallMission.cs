using System;
using Additions.Missions;
using Duckject.Core.Attributes;
using Game.Cooking.Core;
using Game.Cooking.Meals.Data;
using UnityEngine;

namespace Game.Cooking.Missions
{
	public class ClientApproachToStallMission : MissionBase
	{
		[SerializeField] private float desireTime;

		private IClientWishes _clientWishes;
		private WishCloud _wishCloud;

		[Quack]
		private void Construct(IClientWishes clientWishes, WishCloud wishCloud)
		{
			_clientWishes = clientWishes;
			_wishCloud = wishCloud;
		}

		protected override void Mission()
		{
			//TODO Реализовать момент когда подходит клиент

			_clientWishes.InitClientWishes(desireTime);

			InitAndShowWishCloud(End);
		}

		private void InitAndShowWishCloud(Action onComplete)
		{
			(MealData item1, MealData item2) = _clientWishes.Wishes;
			if(item2 == null)
				_wishCloud.Show(item1.MealSprite, onComplete);
			else
				_wishCloud.Show((item1.MealSprite, item2.MealSprite), onComplete);
		}
	}
}