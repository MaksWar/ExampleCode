using System.Collections;
using UnityEngine;

namespace Game.Cooking
{
	public interface ICoroutineRunner
	{
		Coroutine StartCoroutine(IEnumerator coroutine);

		void StopCoroutine(Coroutine coroutine);
	}
}