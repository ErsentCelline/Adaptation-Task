using System;

namespace Domain.Interfaces
{
    public interface ILevelModel : IDisposable
    {
        public void AddExperience(int amount);
    }
}