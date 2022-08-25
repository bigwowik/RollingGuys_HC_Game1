using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.Randomness;
using CodeBase.Infrastructure.States;
using CodeBase.UI.Factory;
using Zenject;

public class SceneInstaller : MonoInstaller, ICoroutineRunner
{
    public override void InstallBindings()
    {
        BindGame();
        BindGameStateMachine();
            
        BindCoroutineRunner();
        BindInputService();
        BindSceneLoader();

        BindProgressService();
        //BindRestartService();
        
        BindRandomService();


        //BindInstallerInterfaces();
        
        BindGameFactory();
        BindUIFactory();
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

    private void BindRestartService()
    {
        // Container
        //     .Bind<IRestartService>()
        //     .To<RestartService>()
        //     .AsSingle();
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
