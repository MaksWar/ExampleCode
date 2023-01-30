using System;
using System.Collections;
using UnityEngine;

namespace Additions.Missions
{
	public abstract class MissionBase : MonoBehaviour
	{
		public event Action<MissionBase> OnMissionEnd;
		public event Action<MissionBase> OnMissionStart;
		public event Action<MissionBase> OnMissionStop;

		protected Coroutine coroutineHelper;

		/// <summary>
		/// Включить миссиию
		/// </summary>
		public void EnableMission()
		{
			gameObject.SetActive(true);

			Mission();
			OnMissionStart?.Invoke(this);
		}

		/// <summary>
		/// Основная функция миссии, вызывается автоматически при вызове метода <see cref="MissionBase.EnableMission()"/>
		/// </summary>
		protected abstract void Mission();

		/// <summary>
		/// Вызывать для завершения миссии.
		/// </summary>
		protected void End()
		{
			gameObject.SetActive(false);

			OnMissionEnd?.Invoke(this);
			OnMissionStop?.Invoke(this);
		}

		/// <summary>
		/// Перезагрузка корутины
		/// </summary>
		public void ReloadCoroutine(IEnumerator routine)
		{
			if (coroutineHelper != null)
				StopCoroutine(coroutineHelper);

			coroutineHelper = StartCoroutine(routine);
		}

		/// <summary>
		/// Отключение подсказки
		/// </summary>
		public void DisableHelperCoroutine() =>
			StopCoroutine(coroutineHelper);

		protected virtual void Reset() =>
			gameObject.name = GetType().Name;
	}
}