using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services.Ads;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using CodeBase.UI.Windows.EndLevelWindow;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IRewardService _rewardService;
        private readonly IAdsRewardedService _adsRewardedService;
        private const string UIRootPath = "UI/UIRoot";
        private const string PauseMenuUIPath = "UI/Pause/Pause UI";
        private const string EndLevelWindowPath = "UI/Windows/EndLevelWindowView";


        private readonly IAssets _assets;
        private readonly IConfigsService _configs;

        private readonly Transform _uiRoot;

        //[Inject(Id = "UIRoot")]
        public UIFactory(RectTransform uiRoot, IRewardService rewardService, IAdsRewardedService adsRewardedService)
        {
            _rewardService = rewardService;
            _adsRewardedService = adsRewardedService;
            _uiRoot = (Transform) uiRoot;
        }
        public void CreateSomeWindow()
        {
            //load config
            
            //instantiate
            
            //constuct
        }

        public void CreateUIRoot()
        {
            // var rootPrefab = Resources.Load<GameObject>(UIRootPath);
            // GameObject uiRoot = GameObject.Instantiate(rootPrefab);
            // _uiRoot = uiRoot.transform;
        }

        public void CreatePauseMenu()
        {
            var prefab = Resources.Load<GameObject>(PauseMenuUIPath);
            var pauseUi = GameObject.Instantiate(prefab, _uiRoot);
        }

        public void CreateStartMenu()
        {
            WDebug.Log(WType.UI, "Create Start Menu");
        }

        public EndLevelWindowPresenter CreateEndLevelWindow(LevelResult levelResult)
        {
            var prefab = Resources.Load<EndLevelWindowView>(EndLevelWindowPath);
            
            var view = GameObject.Instantiate(prefab, _uiRoot);

            EndLevelWindowModel windowModel = new EndLevelWindowModel();
            EndLevelWindowPresenter windowPresenter = new EndLevelWindowPresenter(view, windowModel, _rewardService, _adsRewardedService);

            return windowPresenter;
        }

    }
}