using System;
using Domain.EventData;
using UnityEngine.UIElements;
using MessagePipe;
using Presentation.View.Interfaces;
using UniRx;
using VContainer.Unity;

namespace Presentation.Presenter
{
    public class HeroPresenter : IStartable, IDisposable
    {
        private readonly IHeroView _view;
        private readonly IPublisher<AddExperienceMessage> _requestExperiencePublisher;
        private readonly CompositeDisposable _disposables = new();
        private readonly int _defaultExperienceAmount = 50;

        public HeroPresenter(IHeroView view,
            IPublisher<AddExperienceMessage> requestExperiencePublisher,
            ISubscriber<ExperienceChangedMessage> expSubscriber, 
            ISubscriber<LevelUpMessage> levelUpSubscriber
            )
        {
            _view = view;
            
            _requestExperiencePublisher = requestExperiencePublisher ?? throw new ArgumentNullException(nameof(requestExperiencePublisher));
            
            expSubscriber.Subscribe(_view.UpdateProgressBarValue).AddTo(_disposables);
            levelUpSubscriber.Subscribe(_view.UpdateProgressBarTitle).AddTo(_disposables);
        }
        
        public void Start()
        {
            BindControls();
        }

        private void BindControls()
        {
            _view.AddExperienceButton.RegisterCallback<ClickEvent>(OnClickAddExp);
            _view.BackButton.RegisterCallback<ClickEvent>(OnClickBack);
        }

        private void OnClickBack(ClickEvent clickEvent)
        {
            throw new NotImplementedException();
        }

        private void OnClickAddExp(ClickEvent clickEvent)
        {
            _requestExperiencePublisher.Publish(new AddExperienceMessage(_defaultExperienceAmount));
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}