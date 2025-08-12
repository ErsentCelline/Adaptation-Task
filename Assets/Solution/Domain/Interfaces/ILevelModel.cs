using System;
using UniRx;

namespace Domain.Interfaces
{
    public interface ILevelModel : IDisposable
    {
        public IReadOnlyReactiveProperty<int> CurrentLevel { get; }
        public IReadOnlyReactiveProperty<int> CurrentExperience { get; }
        public IReadOnlyReactiveProperty<int> RequiredExperience { get; }
    }
}