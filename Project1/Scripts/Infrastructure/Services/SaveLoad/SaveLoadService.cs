using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
	public class SaveLoadService : ISaveLoadService
	{
		private const string ProgressKey = "Progress";

		private readonly IPersistentProgressService _progressService;
		private readonly IFactoryContainer _factoryContainer;

		public SaveLoadService(IPersistentProgressService progressService, IFactoryContainer factoryContainer)
		{
			_progressService = progressService;
			_factoryContainer = factoryContainer;
		}

		public void SaveProgress()
		{
			foreach (ISavedProgress progressWriter in _factoryContainer.ProgressWriters)
				progressWriter.UpdateProgress(_progressService.Progress);

			PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
		}

		public PlayerProgress LoadProgress() =>
			PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
	}
}