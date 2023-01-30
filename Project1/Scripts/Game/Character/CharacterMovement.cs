using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Game.Character
{
	public class CharacterMovement : MonoBehaviour
	{
		[SerializeField, HideInInspector] private CharacterController characterController;
		[SerializeField] private float movementSpeed;

		private IInputService _inputService;
		private Camera _camera;

		[Inject]
		private void Construct(IInputService inputService) =>
			_inputService = inputService;

		private void Start() =>
			_camera = Camera.main;

		private void Update()
		{
			var movementVector = Vector3.zero;
			var axis = _inputService.Axis;

			if (axis.sqrMagnitude > Mathf.Epsilon)
			{
				movementVector = _camera.transform.TransformDirection(axis);
				movementVector.y = 0;
				movementVector.Normalize();

				transform.forward = movementVector;
			}

			movementVector += Physics.gravity;

			characterController.Move(movementSpeed * movementVector * Time.deltaTime);
		}

		#region Editor

		private void OnValidate()
		{
			if (characterController == null)
				characterController = GetComponent<CharacterController>();
		}

		#endregion
	}
}