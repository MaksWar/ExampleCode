using System;
using Additions.Extensions;
using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Game.Character
{
	public class CharacterHealth : MonoBehaviour, IHealth, ISavedProgress
	{
		private float _currentHealth;

		public float Current => _currentHealth >= 0 ? _currentHealth : 0;

		public float Max { get; }

		public event Action<float> OnHealthChanged;
		public event Action OnHealthIsOver;

		public void TakeDamage(float health)
		{
			if (health.IsBelowOrEqualZero()) return;

			_currentHealth -= health;

			OnHealthChanged?.Invoke(_currentHealth);
			if (_currentHealth.IsBelowOrEqualZero())
				OnHealthIsOver?.Invoke();
		}

		public void TakeHeal(float health)
		{
			if (health.IsBelowOrEqualZero()) return;

			_currentHealth += health;

			OnHealthChanged?.Invoke(_currentHealth);
		}

		public void LoadProgress(PlayerProgress progress) =>
			_currentHealth = progress.Stats.CurrentHp;

		public void UpdateProgress(PlayerProgress progress) =>
			progress.Stats.CurrentHp = Current;
	}
}