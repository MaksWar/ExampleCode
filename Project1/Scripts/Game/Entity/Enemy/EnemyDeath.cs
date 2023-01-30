using UnityEngine;

namespace Game.Entity.Enemy
{
	public class EnemyDeath : MonoBehaviour
	{
		[SerializeField] private EnemyHealth health;

		private bool _isDead;

		private void Start() =>
			health.OnHealthIsOver += Die;

		private void OnDestroy() =>
			health.OnHealthIsOver -= Die;

		private void Die()
		{
			_isDead = true;
			Debug.Log($"<color=red>{name} : Dead</color>");
		}
	}
}