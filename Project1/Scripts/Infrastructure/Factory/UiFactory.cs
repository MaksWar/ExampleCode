using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factory
{
	public class UiFactory : FactoryBase, IUiFactory
	{
		public UiFactory(IAssetProvider assetProvider) : base(assetProvider)
		{
		}

		public GameObject CreateHUD() =>
			InstantiateRegistered(AssetPath.HUDPath);
	}
}