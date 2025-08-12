using Domain.Interfaces;

namespace Infrastructure.Logic
{
    public abstract class LevelUpLogic
    {
        protected abstract IUpdatableLevelModel UpdatableLevelModel { get; set; }

        public abstract void AddExperience(int amount);
        protected abstract void CheckLevelUp();
        protected abstract int CalculateRequiredExperience(int level);
    }
}