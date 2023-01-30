using System;
using System.Linq;
using PSV;

namespace Additions.Extensions
{
	public static class StringExtensions
	{
		public static Scenes StringToScenes(this string value) =>
			Enum.GetValues(typeof(Scenes)).Cast<Scenes>().ToList().FirstOrDefault(x => x.ToString() == value);
	}
}