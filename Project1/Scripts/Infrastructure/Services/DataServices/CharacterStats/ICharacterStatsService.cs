using Game.Data.CharacterStats;

namespace Infrastructure.Services.DataServices.CharacterStats
{
	public interface ICharacterStatsService : IService
	{
		void Init();

		CharacterStatsData GetCharacterStats();
	}
}