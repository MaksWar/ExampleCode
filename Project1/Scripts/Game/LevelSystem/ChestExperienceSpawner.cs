using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Game.LevelSystem
{
	public class ChestExperienceSpawner : MonoBehaviour
	{
		[SerializeField] private ChestExperiencePoint chestPrefab;
		[SerializeField] private List<Transform> positionsForSpawn;

		[SerializeField] private int countOfSpawn;
		[SerializeField] private float minExperience;
		[SerializeField] private float maxExperience;

		private void Start() =>
			Spawn(countOfSpawn);

		private void Spawn(int count)
		{
			for (int i = 0, j = 0; i < positionsForSpawn.Count && j < count; i++, j++)
			{
				var chest = ExperienceChestPool.Instance.Pop(chestPrefab, transform);

				chest.SetExperienceReward(Random.Range(minExperience, maxExperience));
				chest.transform.position = positionsForSpawn[i].position;
			}
		}
	}
}