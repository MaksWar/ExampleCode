using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.PersistentProgress;

namespace Infrastructure.Factory
{
	public class FactoriesContainer : IFactoryContainer
	{
		private Dictionary<Type, IFactory> _factories = new Dictionary<Type, IFactory>();

		public IReadOnlyList<IFactory> Factories => _factories.Values.ToList();

		public IReadOnlyList<ISavedProgressReader> ProgressReaders
		{
			get
			{
				var progressReaders = new List<ISavedProgressReader>();
				foreach (var readers in Factories)
					readers.ProgressReaders.ForEach(x => progressReaders.Add(x));

				return progressReaders;
			}
		}

		public IReadOnlyList<ISavedProgress> ProgressWriters
		{
			get
			{
				var progressWriters = new List<ISavedProgress>();
				foreach (var writers in Factories)
					writers.SavedProgresses.ForEach(x => progressWriters.Add(x));

				return progressWriters;
			}
		}

		public FactoriesContainer(PlayerFactory playerFactory, UiFactory uiFactory)
		{
			Set<IPlayerFactory>(playerFactory);
			Set<IUiFactory>(uiFactory);
		}

		private IFactory Get<TType>() =>
			_factories[typeof(TType)];

		private void Set<TType>(IFactory factory) =>
			_factories[typeof(TType)] =  factory;
	}
}