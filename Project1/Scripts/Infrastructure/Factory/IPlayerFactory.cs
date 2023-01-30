using UnityEngine;

namespace Infrastructure.Factory
{
	public interface IPlayerFactory
	{
		GameObject CreateHero(GameObject at);
	}
}