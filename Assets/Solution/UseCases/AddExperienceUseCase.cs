using System;
using Domain.Interfaces;
using EventData;
using Infrastructure.Logic;
using MessagePipe;
using UniRx;
using VContainer.Unity;

namespace UseCases
{
    public class AddExperienceUseCase : IStartable, IDisposable
    {
        private readonly LevelUpLogic _levelUpLogic;
        private readonly CompositeDisposable _disposables = new();
        private readonly ISubscriber<AddExperienceMessage> _requestExperienceSubscriber;

        public AddExperienceUseCase(
            ILevelModel levelModel,
            LevelUpLogic levelUpLogic, 
            ISubscriber<AddExperienceMessage> requestExperienceSubscriber, 
            IPublisher<ExperienceChangedMessage> expPublisher, 
            IPublisher<LevelUpMessage> levelUpPublisher)
        {
            _requestExperienceSubscriber = requestExperienceSubscriber;
            _levelUpLogic = levelUpLogic;
            
            levelModel.CurrentLevel.Subscribe(currentLevel => 
                levelUpPublisher.Publish(new LevelUpMessage(currentLevel)));
            
            levelModel.CurrentExperience.Subscribe(currentExp =>
                expPublisher.Publish(new ExperienceChangedMessage(currentExp, levelModel.RequiredExperience.Value)));
        }
        
        public void Start()
        {
            _requestExperienceSubscriber.Subscribe(OnAddExperience).AddTo(_disposables);
        }
        
        private void OnAddExperience(AddExperienceMessage message)
        {
            _levelUpLogic.AddExperience(message.Amount);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}