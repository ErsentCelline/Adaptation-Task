using System;
using Domain.EventData;
using Domain.Interfaces;
using MessagePipe;
using UniRx;
using UnityEngine;

namespace Domain.UseCases
{
    public class AddExperienceUseCase : IDisposable
    {
        private readonly ILevelModel _levelModel;
        private readonly CompositeDisposable _disposables = new();

        public AddExperienceUseCase(ILevelModel levelModel, ISubscriber<AddExperienceMessage> requestExperienceSubscriber)
        {
            _levelModel = levelModel;

            Debug.Log("Subscribing to AddExperienceMessage");
            requestExperienceSubscriber.Subscribe(OnAddExperience).AddTo(_disposables);
            
            _disposables.Add(_levelModel);
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