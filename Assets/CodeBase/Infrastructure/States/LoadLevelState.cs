using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData;
using CodeBase.UI.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingFader _loadingFader;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;


        public LoadLevelState(GameStateMachine stateMachine, 
            ISceneLoader sceneLoader,
            LoadingFader loadingFader,
            IGameFactory gameFactory,
            IStaticDataService staticDataService,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingFader = loadingFader;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            Debug.Log("Enter LoadLevelState");
            _loadingFader.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            Debug.Log("Exit LoadLevelState");
            _loadingFader.Hide();
            LightProbes.TetrahedralizeAsync();
            
            
        }

        private void OnLoaded()
        {
            Debug.Log("OnLoaded LoadLevelState");
            
            InitUiRoot();
            
            //InitGameWorld();

            var heroSpawnPoint = GameObject.FindGameObjectWithTag("Respawn"); //todo remove it

            if (heroSpawnPoint != null)
            {
                InitHero();

                InitHud();

                InitEnemySpawner();
            }

            _stateMachine.Enter<GameLoopState>();
            
        }

        private void InitEnemySpawner()
        {
            var heroSpawnPoint = GameObject.FindGameObjectWithTag("EnemySpawnPoint");
            _gameFactory.CreateEnemySpawner(heroSpawnPoint.transform.position);
        }

        private void InitUiRoot()
        {
            _uiFactory.CreateUIRoot();
        }


        private void InitGameWorld()
        {
            //LevelStaticData levelData = LevelStaticData();

            
            //GameObject hero = InitHero();
            //InitHud(hero);
            
        }

        private void InitHero()
        {
            Debug.Log("Init hero");
            var heroSpawnPoint = GameObject.FindGameObjectWithTag("Respawn");
            _gameFactory.CreateHero(heroSpawnPoint.transform.position);
        }

        private void InitHud()
        {
            GameObject hud = _gameFactory.CreateHud();
            
            _gameFactory.CreateDialogUI();
        }
        




    }
}