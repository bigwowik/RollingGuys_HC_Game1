using System;
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
        private Transform _heroTransform;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
            
        }

        public void StartLevelProgress(Transform heroTransform, IMapCreator mapCreator)
        {
            _mapCreator = mapCreator;
            _heroTransform = heroTransform;
            ProgressFillImage.gameObject.SetActive(true);
        }
        
        private void Update()
        {
            if(!_inputService.isActive) return;
            
            var amount = 1 - (_mapCreator.MapEndPosition - _heroTransform.position.z)/_mapCreator.MapEndPosition;
            ProgressFillImage.fillAmount = amount;
        }
    }
}