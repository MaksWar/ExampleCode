using System;
using System.Collections;
using UnityEngine;

namespace Game.Cooking.Core
{
	public class TimeCounter : ITimeCounter
	{
		private Coroutine _timeCoroutine;

		private float _currentTime;

		private readonly ICoroutineRunner _coroutineRunner;

		public event Action OnTimeChanged;

		public float Seconds => _currentTime;

		public TimeCounter(ICoroutineRunner coroutineRunner) =>
			_coroutineRunner = coroutineRunner;

		public void Start()
		{
			ResetTime();

			_timeCoroutine = _coroutineRunner.StartCoroutine(StartCount());
		}

		public void Stop() =>
			_coroutineRunner.StopCoroutine(_timeCoroutine);

		private void ResetTime() =>
			_currentTime = 0;

		private IEnumerator StartCount()
		{
			while (true)
			{
				yield return new WaitForSecondsRealtime(0.1f);

				_currentTime += 0.1f;

				OnTimeChanged?.Invoke();
			}
		}
	}
}