using DG.Tweening;
using Game.Scripts.Game.Character;
using Pool;
using UnityEngine;

namespace Game.Scripts.Game.LevelSystem
{
	public class ChestExperiencePoint : MonoBehaviourPoolObject
	{
		[SerializeField] private float experienceReward;

		private bool _isPickedUp;

		public void SetExperienceReward(float experience)
		{
			if(experience < 0) return;

			experienceReward = experience;
		}

		public override void Push() =>
			ExperienceChestPool.Instance.Push(this);

		private void PickUp()
		{
			_isPickedUp = true;
			transform
				.DOScale(0f, 2f)
				.SetEase(Ease.InBounce);
		}

		//TODO подумать как его перевести на Physic.Overlap
		private void OnTriggerEnter(Collider other)
		{
			if(_isPickedUp) return;

			var characterLevel = other.GetComponent<ILevel>();
			if (characterLevel == null) return;

			characterLevel.IncreaseExperience(experienceReward);
			PickUp();
		}
	}
}