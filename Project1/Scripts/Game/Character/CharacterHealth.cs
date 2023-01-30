using System;
using Additions.Extensions;
using UnityEngine;

namespace Game.Scripts.Game.Character
{
	public class CharacterHealth : MonoBehaviour
	{
		private float _currentHealth;

		public float Current => _currentHealth;
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
	}
}