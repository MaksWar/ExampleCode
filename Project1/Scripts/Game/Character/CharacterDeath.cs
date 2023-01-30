using UnityEngine;

namespace Game.Character
{
	public class CharacterDeath : MonoBehaviour
	{
		[SerializeField] private CharacterHealth characterHealth;
		[SerializeField] private CharacterMovement characterMovement;

		private bool _isDead;

		private void Start() =>
			characterHealth.OnHealthIsOver += Die;

		private void OnDestroy() =>
			characterHealth.OnHealthIsOver -= Die;

		private void Die()
		{
			_isDead = true;
			characterMovement.enabled = false;
		}
	}
}