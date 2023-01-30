using System;

namespace Game.Cooking.Core
{
	public interface ITimeCounter
	{
		public event Action OnTimeChanged;

		float Seconds { get; }

		void Start();

		void Stop();
	}
}