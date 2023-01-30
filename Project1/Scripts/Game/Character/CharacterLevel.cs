using System;
using UnityEngine;

namespace Game.Scripts.Game.Character
{
	public class CharacterLevel : MonoBehaviour, ILevel
	{
		[SerializeField] private float targetExpToLevelUp = 10;
		[Tooltip("Коефіціент задля підняття кол-ва необхідних очків опиту, після підвищення рівня")]
		[SerializeField] private float coefOfTargetExp = 2.5f;

		private float _levelPoints;
		private int _currentLevel;

		public int CurrentLevel => _currentLevel;

		public event Action OnLevelUp;

		public void IncreaseExperience(float exp)
		{
			_levelPoints += exp;

			for (; _levelPoints >= targetExpToLevelUp;)
			{
				_levelPoints -= targetExpToLevelUp;

				LevelUp();
			}
		}

		private void LevelUp()
		{
			_currentLevel++;

			IncreaseTargetExperience();

			OnLevelUp?.Invoke();
		}

		private void IncreaseTargetExperience() =>
			targetExpToLevelUp *= coefOfTargetExp;
	}
}