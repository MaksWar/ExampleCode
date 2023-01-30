using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
	public abstract class FactoryBase : IFactory
	{
		protected readonly IAssetProvider _assetProvider;

		public List<ISavedProgressReader> ProgressReaders { get; }
		public List<ISavedProgress> SavedProgresses { get; }

		protected FactoryBase(IAssetProvider assetProvider) =>
			_assetProvider = assetProvider;

		protected GameObject InstantiateRegistered(string pathPrefab, Vector3 pos)
		{
			GameObject obj = _assetProvider.Instantiate(pathPrefab, pos);
			RegisterProgressWatchers(obj);

			return obj;
		}

		protected GameObject InstantiateRegistered(string pathPrefab)
		{
			GameObject obj = _assetProvider.Instantiate(pathPrefab);
			RegisterProgressWatchers(obj);

			return obj;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(progressReader);
		}

		private void Register(ISavedProgressReader progressReader)
		{
			if(progressReader is ISavedProgress)
				SavedProgresses.Add((ISavedProgress) progressReader);

			ProgressReaders.Add(progressReader);
		}
	}
}