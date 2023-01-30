using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factory
{
	public class PlayerFactory : FactoryBase, IPlayerFactory
	{
		public PlayerFactory(IAssetProvider assetProvider) : base(assetProvider)
		{
		}

		public GameObject CreateHero(GameObject at) =>
			InstantiateRegistered(AssetPath.CharacterPath, at.transform.position);
	}
}