namespace Domain.EventData
{
    public class AddExperienceMessage
    {
        public AddExperienceMessage(int amount)
        {
            Amount = amount;
        }
        
        public int Amount { get; private set; }
    }
}