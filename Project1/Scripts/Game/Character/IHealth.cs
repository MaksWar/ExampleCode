using System;

namespace Game.Character
{
	public interface IHealth
	{
		public float Current { get; }
		public float Max { get; }

		public event Action<float> OnHealthChanged;
		public event Action OnHealthIsOver;

		void TakeDamage(float health);

		void TakeHeal(float health);
	}
}