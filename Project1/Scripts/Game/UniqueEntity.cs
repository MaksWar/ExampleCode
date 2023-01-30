using UnityEngine;

namespace Game
{
	public class UniqueEntity : MonoBehaviour
	{
		[SerializeField] private EntityType typeOfEntity;

		public EntityType Type => typeOfEntity;
	}

	public enum EntityType
	{
		Character = 0,
		Enemy = 1,
		Neutral = 2,
	}
}