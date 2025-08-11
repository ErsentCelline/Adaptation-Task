using Domain.EventData;
using UnityEngine.UIElements;

namespace Presentation.View.Interfaces
{
    public interface IHeroView
    {
        void UpdateProgressBarValue(ExperienceChangedMessage message);
        void UpdateProgressBarTitle(LevelUpMessage message);
        Button AddExperienceButton { get; }
        Button BackButton { get; }
    }
}