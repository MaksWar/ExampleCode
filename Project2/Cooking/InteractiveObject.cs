using Additions.Effects;
using UnityEngine;

namespace Game.Cooking
{
	public class InteractiveObject : MonoBehaviour
	{
		[SerializeField] private Effect effect;

		private void OnMouseDown() =>
			effect.StartEffect();
	}
}