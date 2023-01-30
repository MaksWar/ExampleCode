using UnityEngine;

namespace Additions.CameraUtils
{
	[RequireComponent(typeof(Camera))]
	public class CameraFollow2D : MonoBehaviour
	{
		[SerializeField] private Transform target;
		[SerializeField] private Vector2 offset = Vector2.zero;
		[SerializeField] private SpriteRenderer fitSprite;
		[SerializeField] private float speedModifier = 1f;
		[SerializeField] private bool followY = true;

		[SerializeField, HideInInspector] private Camera cam;

		private (Vector3 min, Vector3 max) _bounds;

		private void Start() =>
			CalculateBounds();

		public void SetTarget(Transform target) =>
			this.target = target;

		public void SetSpeedModifier(float newModifier) =>
			speedModifier = newModifier;

		private void LateUpdate()
		{
			if (target == null) return;

			var position = transform.position;
			var targetPoint = new Vector3(target.position.x + offset.x,
				(followY ? target.position.y + offset.y : transform.position.y),
				position.z);

			position = Vector3.Lerp(position, targetPoint,
				1 / Vector3.Distance(position, targetPoint) * speedModifier);
			transform.position = position;

			FitInSpriteBounds();
		}

		private void CalculateBounds()
		{
			if (fitSprite == false)
				return;

			var bounds = fitSprite.bounds;
			_bounds = (bounds.min, bounds.max);
		}

		private void FitInSpriteBounds()
		{
			if (!fitSprite)
				return;

			var position = transform.position;

			Vector3 leftBottom = cam.ViewportToWorldPoint(Vector3.zero);
			Vector3 rightTop = cam.ViewportToWorldPoint(Vector3.one);

			float xPos = position.x;
			float yPos = position.y;

			if (leftBottom.x < _bounds.min.x)
				xPos = position.x + (_bounds.min.x - leftBottom.x);
			else if (rightTop.x > _bounds.max.x)
				xPos = position.x + (_bounds.max.x - rightTop.x);

			if (leftBottom.y < _bounds.min.y)
				yPos = position.y + (_bounds.min.y - leftBottom.y);
			else if (rightTop.y > _bounds.max.y)
				yPos = position.y + (_bounds.max.y - rightTop.y);

			transform.position = new Vector3(xPos, yPos, position.z);
		}

		#region Editor

		private void OnValidate()
		{
			if (cam == null)
				cam = GetComponent<Camera>();
		}

		#endregion
	}
}