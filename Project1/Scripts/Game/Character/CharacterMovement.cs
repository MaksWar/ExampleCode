using Game.Enemy;
using Infrastructure.Data;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Game.Character
{
	public class CharacterMovement : MonoBehaviour, ISavedProgressReader
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

		public void LoadProgress(PlayerProgress progress) =>
			movementSpeed = progress.Stats.MoveSpeed;

		#region Editor

		private void OnValidate()
		{
			if (characterController == null)
				characterController = GetComponent<CharacterController>();
		}

		#endregion
	}
}