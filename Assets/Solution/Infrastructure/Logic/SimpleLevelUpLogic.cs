using Domain.Interfaces;
using UnityEngine;

namespace Infrastructure.Logic
{
    public sealed class SimpleLevelUpLogic : LevelUpLogic
    {
        public SimpleLevelUpLogic(IUpdatableLevelModel updatableLevelModel)
        {
            UpdatableLevelModel = updatableLevelModel;
        }

        protected override IUpdatableLevelModel UpdatableLevelModel { get; set; }

        public override void AddExperience(int amount)
        {
            UpdatableLevelModel.SetExperience(UpdatableLevelModel.CurrentExperience.Value + amount);
            CheckLevelUp();
        }

        protected override void CheckLevelUp()
        {
            while (UpdatableLevelModel.CurrentExperience.Value >= UpdatableLevelModel.RequiredExperience.Value)
            {
                UpdatableLevelModel.SetExperience(UpdatableLevelModel.CurrentExperience.Value - UpdatableLevelModel.RequiredExperience.Value);
                UpdatableLevelModel.SetLevel(UpdatableLevelModel.CurrentLevel.Value + 1);
                UpdatableLevelModel.SetRequiredExperience(CalculateRequiredExperience(UpdatableLevelModel.CurrentLevel.Value));
            }
        }

        protected override int CalculateRequiredExperience(int level)
        {
            return 100 * Mathf.RoundToInt(Mathf.Pow(1.5f, level - 1));
        }
    }
}