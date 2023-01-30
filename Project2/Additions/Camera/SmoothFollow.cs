using UnityEngine;

namespace Additions.CameraUtils
{
	public class SmoothFollow : MonoBehaviour
	{
		[SerializeField] protected Transform target;
		[SerializeField] protected Vector3 offset = new Vector3(1.3f, 1.81f, -10);
		[SerializeField] protected float speed = 0.1f;
		[SerializeField] protected bool followY;

		public Vector3 Offset => offset;

		public void SetTarget(Transform newTarget) => target = newTarget;
		
		protected virtual void LateUpdate()
		{
			if (target == null) return;

			Vector3 desiredPosition = new Vector3(target.position.x + offset.x,
				followY ? target.position.y + offset.y : transform.position.y,
				transform.position.z);
			
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
			transform.position = smoothedPosition;
		}
	}
}