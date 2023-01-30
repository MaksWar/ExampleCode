using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
	public class GameRunner: MonoBehaviour
	{
		public static string DebugScene;

		[SerializeField] private GameBootstrapper gameBootstrapperPrefab;

		private void Awake()
		{
			DebugScene = SceneManager.GetActiveScene().name;

			var gameBootstrapper = FindObjectOfType<GameBootstrapper>();
			if (gameBootstrapper == null)
				SceneManager.LoadScene(Scenes.InitialScene);
		}
	}
}