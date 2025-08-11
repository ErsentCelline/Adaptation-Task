namespace Domain.EventData
{
    public class ExperienceChangedMessage
    {
        public ExperienceChangedMessage(int currentExperience, int newRequiredExperience)
        {
            CurrentValue = currentExperience;
            NewRequiredExperience = newRequiredExperience;
        }
        
        public int CurrentValue { get; }
        public int NewRequiredExperience { get; }
    }
}