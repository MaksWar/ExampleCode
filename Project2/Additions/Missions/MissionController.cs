using System.Collections.Generic;
using UnityEngine;
using PSV;
using System;
using System.Linq;

namespace Additions.Missions
{
	public class MissionController : MonoBehaviour
	{
		[Header("Сцена после окончания миссий")] [SerializeField]
		private Scenes nextScene = Scenes.MainMenu;

		[Header("Последовательный список миссий")] [SerializeField]
		protected List<MissionBase> missionSequence = new List<MissionBase>();

		protected int Index;

		public event Action OnMissionEnded;

		private void OnEnable() =>
			missionSequence.ForEach(item => item.OnMissionEnd += OnMissionEnd);

		private void OnDisable() =>
			missionSequence.ForEach(item => item.OnMissionEnd -= OnMissionEnd);

		private void Start() =>
			missionSequence[Index].EnableMission();

		public Type GetCurrentMissionType() =>
			missionSequence[Index].GetType();

		public T GetMission<T>()
		{
			MissionBase mission = missionSequence.FirstOrDefault(item => item.GetType() == typeof(T));
			if (mission is T)
				return (T) Convert.ChangeType(mission, typeof(T));

			return default;
		}

		protected virtual void OnMissionEnd(MissionBase obj)
		{
			CallOnMissionEnded();

			if (Index < missionSequence.Count - 1)
				NextMission();
			else
				LoadNextScene();
		}

		protected virtual void NextMission()
		{
			Index++;
			missionSequence[Index].EnableMission();
		}

		protected void CallOnMissionEnded() =>
			OnMissionEnded?.Invoke();

		protected void LoadNextScene()
		{
			if (nextScene != Scenes.None)
				SceneLoader.SwitchToScene(nextScene);
			else
				SceneLoader.SwitchToScene(Scenes.MainMenu);
		}
	}
}