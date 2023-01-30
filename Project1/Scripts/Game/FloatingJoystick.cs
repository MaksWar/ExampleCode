using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Game
{
	public class FloatingJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		[SerializeField] private Joystick joystick;

		public void OnPointerDown(PointerEventData eventData) =>
			joystick.OnPointerDown(eventData);

		public void OnDrag(PointerEventData eventData) =>
			joystick.OnDrag(eventData);

		public void OnPointerUp(PointerEventData eventData) =>
			joystick.OnPointerUp(eventData);
	}
}