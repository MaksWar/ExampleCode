using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Additions.Extensions
{
	public static class ListExtension
	{
		public static IEnumerable<Vector3> Positions(this List<Transform> transforms)
			=> transforms.Select(x => x.transform.position);

		public static IEnumerable<Vector3> LocalPositions(this List<Transform> transforms)
			=> transforms.Select(x => x.transform.localPosition);
	}
}