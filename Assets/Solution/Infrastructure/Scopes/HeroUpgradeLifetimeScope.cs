using Domain.Interfaces;
using Domain.Models;
using MessagePipe;
using EventData;
using Presentation.Presenter;
using Presentation.View;
using Presentation.View.Interfaces;
using UseCases;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Scopes
{
    public class HeroUpgradeLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
        
            builder.RegisterMessageBroker<AddExperienceMessage>(options);
            builder.RegisterMessageBroker<ExperienceChangedMessage>(options);
            builder.RegisterMessageBroker<LevelUpMessage>(options);
            
            builder.RegisterComponentInHierarchy<HeroView>().As<IHeroView>();
            
            builder.RegisterEntryPoint<AddExperienceUseCase>();
            builder.Register<ILevelModel, LevelModel>(Lifetime.Singleton);
            builder.Register<IHeroModel, HeroModel>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<HeroPresenter>();
        }
    }
}