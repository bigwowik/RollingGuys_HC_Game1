using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Logic.Map;
using CodeBase.Logic.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Hud
{
    public class LevelProgressHud : MonoBehaviour
    {
        public Image ProgressFillImage;
        private IMapCreator _mapCreator;
        private IInputService _inputService;
        private ILevelProgressService _levelProgressService;

        [Inject]
        public void Construct(IInputService inputService, ILevelProgressService levelProgressService)
        {
            _levelProgressService = levelProgressService;
            _inputService = inputService;
        }

        private void Start()
        {
            ProgressFillImage.fillAmount = 0;
        }

        public void StartLevelProgress(IMapCreator mapCreator)
        {
            _mapCreator = mapCreator;
            ProgressFillImage.gameObject.SetActive(true);
        }
        
        private void Update()
        {
            if(!_inputService.isActive) return;
            
            var amount = 1 - (_mapCreator.MapEndPosition - _levelProgressService.ActivePlayer.position.z)/_mapCreator.MapEndPosition;
            ProgressFillImage.fillAmount = amount;
        }
    }
}