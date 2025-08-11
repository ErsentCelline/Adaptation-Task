using Domain.EventData;

namespace Domain.Models
{
    public class HeroModel
    {
        public HeroModel(LevelModel levelModel)
            => Level = levelModel;
        
        public LevelModel Level { get; private set; }
    }
}