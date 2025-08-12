namespace Domain.Interfaces
{
    public interface IHeroModel
    {
        public ILevelModel LevelModel { get; }
        public IStatsModel StatsModel { get; }
    }
}