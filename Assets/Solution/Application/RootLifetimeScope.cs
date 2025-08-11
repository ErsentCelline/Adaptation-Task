using Domain.EventData;
using Domain.Interfaces;
using Domain.Models;
using MessagePipe;
using Presentation;
using Domain.UseCases;
using Presentation.Presenter;
using Presentation.View.Interfaces;
using VContainer;
using VContainer.Unity;

namespace Application
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
        
            builder.RegisterMessageBroker<AddExperienceMessage>(options);
            builder.RegisterMessageBroker<ExperienceChangedMessage>(options);
            builder.RegisterMessageBroker<LevelUpMessage>(options);
            
            builder.Register<ILevelModel, LevelModel>(Lifetime.Singleton);
            builder.Register<HeroModel>(Lifetime.Singleton);

            builder.Register<AddExperienceUseCase>(Lifetime.Singleton);
            builder.RegisterEntryPoint<HeroPresenter>();

            builder.RegisterComponentInHierarchy<HeroView>().As<IHeroView>();
        }
    }
}