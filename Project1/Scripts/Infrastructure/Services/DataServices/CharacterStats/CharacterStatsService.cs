using Game.Data.CharacterStats;
using Infrastructure.Services.ScriptableLoader;

namespace Infrastructure.Services.DataServices.CharacterStats
{
	public class CharacterStatsService : ICharacterStatsService
	{
		private readonly IScriptableProvider _scriptableProvider;

		private CharacterStatsData _characterStats;

		private const string CharacterStatsDataPath = "Data/CharacterData/MainCharacter";

		public CharacterStatsService(IScriptableProvider scriptableProvider) =>
			_scriptableProvider = scriptableProvider;

		public void Init() =>
			_characterStats = _scriptableProvider.Load<CharacterStatsData>(CharacterStatsDataPath);

		public CharacterStatsData GetCharacterStats() =>
			_characterStats;
	}
}