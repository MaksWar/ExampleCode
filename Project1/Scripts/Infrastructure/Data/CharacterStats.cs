using System;

namespace Infrastructure.Data
{
	[Serializable]
	public class CharacterStats
	{
		public float CurrentHp;
		public float MaxHP;

		public float MoveSpeed;

		public void ResetHp() =>
			CurrentHp = MaxHP;
	}
}