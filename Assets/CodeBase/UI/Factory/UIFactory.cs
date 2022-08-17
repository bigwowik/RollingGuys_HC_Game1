using CodeBase.Infrastructure.AssetManagment;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private const string PauseMenuUIPath = "UI/Pause/Pause UI";

        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;

        private Transform _uiRoot;

        public UIFactory(IAssets assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }
        public void CreateSomeWindow()
        {
            //load config
            
            //instantiate
            
            //constuct
        }

        public void CreateUIRoot()
        {
            var rootPrefab = Resources.Load<GameObject>(UIRootPath);
            GameObject uiRoot = GameObject.Instantiate(rootPrefab);
            _uiRoot = uiRoot.transform;
        }

        public void CreatePauseMenu()
        {
            var prefab = Resources.Load<GameObject>(PauseMenuUIPath);
            var pauseUi = GameObject.Instantiate(prefab, _uiRoot);
        }

    }
}