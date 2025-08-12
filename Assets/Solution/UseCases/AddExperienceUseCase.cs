using System;
using Domain.Interfaces;
using EventData;
using MessagePipe;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace UseCases
{
    public class AddExperienceUseCase : IStartable, IDisposable
    {
        private readonly ILevelModel _levelModel;
        private readonly CompositeDisposable _disposables = new();
        private readonly ISubscriber<AddExperienceMessage> _requestExperienceSubscriber;

        public AddExperienceUseCase(ILevelModel levelModel, ISubscriber<AddExperienceMessage> requestExperienceSubscriber)
        {
            _levelModel = levelModel;

            Debug.Log("Subscribing to AddExperienceMessage");
            _requestExperienceSubscriber = requestExperienceSubscriber;
        }
        
        public void Start()
        {
            _requestExperienceSubscriber.Subscribe(OnAddExperience).AddTo(_disposables);
        }
        
        private void OnAddExperience(AddExperienceMessage message)
        {
            Debug.Log("Use case");
            _levelModel.AddExperience(message.Amount);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}