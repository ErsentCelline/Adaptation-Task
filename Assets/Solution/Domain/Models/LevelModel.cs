using Domain.Interfaces;
using UniRx;

namespace Domain.Models
{
    public class LevelModel : ILevelModel, IUpdatableLevelModel
    {
        private readonly ReactiveProperty<int> _currentLevel;
        private readonly ReactiveProperty<int> _currentExperience;
        private readonly ReactiveProperty<int> _requiredExperience;
        
        private readonly CompositeDisposable _disposables = new();

        public LevelModel()
        {
            _currentLevel = new ReactiveProperty<int>(1).AddTo(_disposables);
            _currentExperience = new ReactiveProperty<int>(20).AddTo(_disposables);
            _requiredExperience = new ReactiveProperty<int>(100).AddTo(_disposables);
        }

        public void SetExperience(int experience)
        {
            _currentExperience.Value = experience;
        }

        public void SetLevel(int level)
        {
            _currentLevel.Value = level;
        }

        public void SetRequiredExperience(int experience)
        {
            _requiredExperience.Value = experience;
        }
        
        public IReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
        public IReadOnlyReactiveProperty<int> CurrentExperience => _currentExperience;
        public IReadOnlyReactiveProperty<int> RequiredExperience => _requiredExperience;
        
        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}