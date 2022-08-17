using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Randomness;
using CodeBase.Sounds;
using CodeBase.StaticData;
using CodeBase.UI.Factory;
using CodeBase.UI.Windows;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, ISceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();

        }

        public void Enter()
        {
            //_sceneLoader.Load(Initial, onLoad: null);
        }

        public void Exit()
        {
        }

        // private void EnterLoadLevel() =>
        //     _stateMachine.Enter<LoadLevelState, string>(Initial);

        private void RegisterServices()
        {
            RegisterStaticData();

            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IRandomService>(new UnityRandomService());
            _services.RegisterSingle(InputService());
            _services.RegisterSingle(_sceneLoader);
            
            
            _services.RegisterSingle<IAudioService>(new AudioService());

            RegisterUIFactory();
            
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));

            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssets>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IRandomService>()
                )
            );
        }

        private void RegisterUIFactory()
        {
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                    _services.Single<IAssets>(),
                    _services.Single<IStaticDataService>()
                )
            );
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            
            _services.RegisterSingle(staticData);
        }
        private static IInputService InputService()
        {
            return new StandaloneInputService();
        }
    }
}