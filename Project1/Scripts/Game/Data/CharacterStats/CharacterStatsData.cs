using UnityEngine;

namespace Game.Data.CharacterStats
{
	[CreateAssetMenu(menuName = "Data/Create CharacterStatsData", fileName = "CharacterStatsData", order = 0)]
	public class CharacterStatsData : DataBase
	{
		[SerializeField] private float maxHp;
		[SerializeField] private float moveSpeed;

		public float MaxHp => maxHp;

		public float MoveSpeed => moveSpeed;
	}
}