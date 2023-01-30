using UnityEngine;
using Zenject;

namespace Infrastructure.AssetManagement
{
	public class AssetProvider : IAssetProvider
	{
		private readonly DiContainer _diContainer;

		public AssetProvider(DiContainer diContainer) =>
			_diContainer = diContainer;

		public GameObject Instantiate(string path) =>
			_diContainer.InstantiatePrefab(LoadPrefabFromResources(path));

		public GameObject Instantiate(string path, Vector3 at) =>
			_diContainer.InstantiatePrefab(LoadPrefabFromResources(path), at, Quaternion.identity, null);

		public GameObject Instantiate(string path, Vector3 at, Quaternion quaternion) =>
			_diContainer.InstantiatePrefab(LoadPrefabFromResources(path), at, quaternion, null);

		public GameObject Instantiate(string path, Vector3 at, Quaternion quaternion, Transform parent) =>
			_diContainer.InstantiatePrefab(LoadPrefabFromResources(path), at, quaternion, parent);

		private GameObject LoadPrefabFromResources(string path) =>
			Resources.Load<GameObject>(path);
	}
}