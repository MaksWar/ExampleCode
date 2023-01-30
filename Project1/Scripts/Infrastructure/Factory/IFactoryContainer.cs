using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;

namespace Infrastructure.Factory
{
	public interface IFactoryContainer : IService
	{
		IReadOnlyList<IFactory> Factories { get; }

		IReadOnlyList<ISavedProgressReader> ProgressReaders { get; }

		IReadOnlyList<ISavedProgress> ProgressWriters { get; }
	}
}