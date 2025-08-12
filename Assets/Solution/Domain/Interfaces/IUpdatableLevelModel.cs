namespace Domain.Interfaces
{
    public interface IUpdatableLevelModel : ILevelModel
    {
        public void SetExperience(int experience);

        public void SetLevel(int level);

        public void SetRequiredExperience(int experience);
    }
}