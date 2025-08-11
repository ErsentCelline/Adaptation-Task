using System.Threading;
using Domain.EventData;
using Presentation.View.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace Presentation.View
{
    [RequireComponent(typeof(UIDocument))]
    public class HeroView : MonoBehaviour, IHeroView
    {
        private UIDocument _uiDocument;
        private VisualElement _root;
        private CancellationTokenSource _animationCts;

        private ProgressBar _progressBar;
        public Button AddExperienceButton { get; private set; }
        public Button BackButton { get; private set; }

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            
            AddExperienceButton = _root.Q<Button>("B_LevelUp");
            BackButton = _root.Q<Button>("B_Back");
            _progressBar = _root.Q<ProgressBar>("ExpProgressBar");
        }

        public void UpdateProgressBarValue(ExperienceChangedMessage message)
        {
            _animationCts?.Cancel();
            _animationCts = new CancellationTokenSource();
        
            _progressBar.value = message.CurrentValue;
            _progressBar.highValue = message.NewRequiredExperience;
        }

        public void UpdateProgressBarTitle(LevelUpMessage message)
        {
            _progressBar.title = $"Level: {message.NewLevel}";
        }
        
        private void OnDestroy()
        {
            _animationCts?.Cancel();
            _animationCts?.Dispose();
        }
    }
}