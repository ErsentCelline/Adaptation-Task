namespace EventData
{
    public class LevelUpMessage
    {
        public LevelUpMessage(int newLevel)
        {
            NewLevel = newLevel;
        }
        
        public int NewLevel { get; }
    }
}