using Game.Data;

namespace Infrastructure.Services.ScriptableLoader
{
	public interface IScriptableProvider : IService
	{
		TType Load<TType>(string path) where TType : DataBase;

		TType[] LoadAll<TType>(string path) where TType : DataBase;
	}
}