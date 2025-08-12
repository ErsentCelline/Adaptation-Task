using Domain.Interfaces;

namespace Domain.Models
{
    public class HeroModel : IHeroModel
    {
        public HeroModel(LevelModel levelModel)
            => LevelModel = levelModel;
        
        public ILevelModel LevelModel { get; }
        public IStatsModel StatsModel { get; }
    }
}