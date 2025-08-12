using Infrastructure;
using UniRx;

namespace Domain.Interfaces
{
    public interface IStatsModel
    {
        public IReadOnlyReactiveDictionary<Enums.StatType, int> Stats { get; }
    }
}