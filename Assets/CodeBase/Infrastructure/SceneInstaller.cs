using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.Randomness;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Map;
using CodeBase.StaticData;
using CodeBase.UI.Factory;
using Zenject;

public class SceneInstaller : MonoInstaller, ICoroutineRunner
{
    public override void InstallBindings()
    {
        //game
        BindGame();
        BindGameStateMachine();
        //loading
        BindSceneLoader();
        BindCoroutineRunner();
        //others
        BindInputService();
        BindRandomService();
        //progress
        BindProgressService();
        BindLevelGameProgress();
        BindSaveLoadDataService();
        //factory
        BindGameFactory();
        BindUIFactory();
        //configs
        BindConfigsService();
        BindMapProvider();

    }

    private void BindMapProvider()
    {
        Container
            .Bind<IMapProvider>()
            .To<MapProviderFromConfigs>()
            .AsSingle();
    }

    private void BindSaveLoadDataService()
    {
        Container
            .Bind<ISaveLoadService<ProgressData>>()
            .To<SaveLoadService<ProgressData>>()
            .AsSingle();
    }

    private void BindConfigsService()
    {
        Container
            .Bind<IConfigsService>()
            .To<ConfigsService>()
            .AsSingle();
    }

    private void BindLevelGameProgress()
    {
        Container
            .Bind<ILevelProgressService>()
            .To<LevelProgressService>()
            .AsSingle();
    }

    private void BindRandomService()
    {
        Container
            .Bind<IRandomService>()
            .To<UnityRandomService>()
            .AsSingle();
    }

    private void BindProgressService()
    {
        Container
            .Bind<IProgressService>()
            .To<ProgressService>()
            .AsSingle();
    }

    private void BindGame()
    {
        Container
            .Bind<Game>()
            .AsSingle().NonLazy();
    }

    private void BindGameStateMachine()
    {
        //states
        Container.Bind<BootstrapState>().AsSingle();

        Container.Bind<LoadDataState>().AsSingle();
        
        Container.Bind<LoadLevelState>().AsSingle();

        Container.Bind<MainMenuState>().AsSingle();
        
        Container.Bind<GameLoopState>().AsSingle();
        
        Container.Bind<EndLevelState>().AsSingle();
     
        //state machine
        Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
    }
    
    private void BindInstallerInterfaces()
    {
        Container
            .BindInterfacesTo<SceneInstaller>()
            .FromInstance(this)
            .AsSingle();
    }

    private void BindInputService()
    {
        Container
            .Bind<IInputService>()
            .To<MouseInputService>()
            .AsSingle();
    }


    private void BindGameFactory()
    {
        Container
            .Bind<IGameFactory>()
            .To<GameFactory>()
            .AsSingle();
    }
    private void BindUIFactory()
    {
        Container
            .Bind<IUIFactory>()
            .To<UIFactory>()
            .AsSingle();
    }

    private void BindCoroutineRunner()
    {
        Container
            .Bind<ICoroutineRunner>()
            .FromInstance(this)
            .AsSingle();
    }

    private void BindSceneLoader()
    {
        Container
            .Bind<SceneLoader>()
            .AsSingle();
    }
}
