using Game.Data;
using UnityEngine;

namespace Infrastructure.Services.ScriptableLoader
{
	public class ScriptableProvider : IScriptableProvider
	{
		public TType Load<TType>(string path) where TType : DataBase =>
			Resources.Load<TType>(path);

		public TType[] LoadAll<TType>(string path) where TType : DataBase =>
			Resources.LoadAll<TType>(path);
	}
}