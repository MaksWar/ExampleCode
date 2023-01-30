using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;

namespace Infrastructure.Factory
{
	public interface IFactory : IService
	{
		List<ISavedProgressReader> ProgressReaders { get; }

		List<ISavedProgress> SavedProgresses { get; }
	}
}