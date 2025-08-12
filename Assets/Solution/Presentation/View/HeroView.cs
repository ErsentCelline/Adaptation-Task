using System.Threading;
using Cysharp.Threading.Tasks;
using EventData;
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
            _progressBar.highValue = message.NewRequiredExperience;
            
            UpdateProgressBarAsync(message).Forget();
        }

        public void UpdateProgressBarTitle(LevelUpMessage message)
        {
            _progressBar.title = $"Level: {message.NewLevel}";
        }

        private async UniTask UpdateProgressBarAsync(ExperienceChangedMessage message, CancellationToken cancellationToken = default)
        {
            _animationCts?.Cancel();
            _animationCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            
            float startValue = _progressBar.value;
            float elapsedTime = 0;
            float duration = .15f;
            
            while (elapsedTime < duration)
            {
                cancellationToken.ThrowIfCancellationRequested();
            
                float progress = elapsedTime / duration;
                float currentValue = Mathf.Lerp(startValue, message.CurrentValue, progress);
            
                _progressBar.value = currentValue;
            
                elapsedTime += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }
        }

        public void Dispose()
        {
            _animationCts?.Cancel();
            _animationCts?.Dispose();
        }
    }
}