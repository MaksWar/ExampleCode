using System;

namespace Game.Scripts.Game.Character
{
	public interface ILevel
	{
		public event Action OnLevelUp;

		public int CurrentLevel { get; }

		void IncreaseExperience(float exp);
	}
}