using Infrastructure;

namespace Domain.Interfaces
{
    public interface IUpdatableStatsModel : IStatsModel
    {
        public void SetStat(Enums.StatType type, int value);
    }
}