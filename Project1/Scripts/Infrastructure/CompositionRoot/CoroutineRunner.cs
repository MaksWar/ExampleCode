using Infrastructure;
using UnityEngine;

namespace Infrastructure.CompositionRoot
{
	public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
	{
		private void Awake() =>
			DontDestroyOnLoad(this);
	}
}