using Domain.Interfaces;
using Infrastructure;
using UniRx;

namespace Domain.Models
{
    public class StatsModel : IUpdatableStatsModel
    {
        public const int MaxStatValue = 10;

        private readonly ReactiveDictionary<Enums.StatType, int> _stats;

        public StatsModel()
        {
            _stats = new ReactiveDictionary<Enums.StatType, int>
            {
                { Enums.StatType.Agility, 1 },
                { Enums.StatType.Strength, 1 },
                { Enums.StatType.Intelligence, 1 }
            };
        }

        public IReadOnlyReactiveDictionary<Enums.StatType, int> Stats { get; }
        public void SetStat(Enums.StatType type, int value)
        {
            _stats[type] = value;
        }
    }
}