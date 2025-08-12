using EventData;
using Domain.Interfaces;
using MessagePipe;
using UniRx;
using UnityEngine;

namespace Domain.Models
{
    public class LevelModel : ILevelModel
    {
        private readonly ReactiveProperty<int> _currentLevel;
        private readonly ReactiveProperty<int> _currentExperience;
        private readonly ReactiveProperty<int> _requiredExperience;
        
        private readonly CompositeDisposable _disposables = new();

        public LevelModel(
            IPublisher<ExperienceChangedMessage> expPublisher,
            IPublisher<LevelUpMessage> levelUpPublisher)
        {
            Debug.Log("LevelModel created");
            
            _currentLevel = new ReactiveProperty<int>(1).AddTo(_disposables);
            _currentExperience = new ReactiveProperty<int>(20).AddTo(_disposables);
            _requiredExperience = new ReactiveProperty<int>(100).AddTo(_disposables);
            
            _currentLevel.Subscribe(level => levelUpPublisher.Publish(
                new LevelUpMessage(level)));
            
            _currentExperience.Subscribe(currentExp => expPublisher.Publish(
                new ExperienceChangedMessage(currentExp, _requiredExperience.Value)));
        }
        
        public void AddExperience(int amount)
        {
            _currentExperience.Value += amount;
            
            CheckLevelUp();
            
            Debug.Log($"Level: {_currentLevel.Value}; Exp: {_currentExperience.Value}/{_requiredExperience.Value}");
        }

        private void CheckLevelUp()
        {
            while (_currentExperience.Value >= _requiredExperience.Value)
            {
                _currentExperience.Value -= _requiredExperience.Value;
                _currentLevel.Value++;
                _requiredExperience.Value = CalculateRequiredExperience(_currentLevel.Value);
            }
        }
        
        private int CalculateRequiredExperience(int level)
        {
            return 100 * Mathf.RoundToInt(Mathf.Pow(1.5f, level - 1));
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}