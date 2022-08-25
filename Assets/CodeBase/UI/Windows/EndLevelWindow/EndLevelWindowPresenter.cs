using System;
using CodeBase.Infrastructure.States;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.UI.Windows.EndLevelWindow
{
    public class EndLevelWindowPresenter 
    {
        private readonly EndLevelWindowView _view;
        private readonly EndLevelWindowModel _model;
        public event Action Closed;

        public EndLevelWindowPresenter(EndLevelWindowView view, EndLevelWindowModel model)
        {
            _view = view;
            _model = model;
         
            _model.MainButtonAction = () =>
            {
                Debug.Log("Main Button Action");
                CloseWindow();
            };
            
            _model.AdditionalButtonAction = () =>
            {
                Debug.Log("Additional Button Action");
                CloseWindow();
            };
        }

        private void CloseWindow()
        {
            GameObject.Destroy(_view.gameObject);
            Closed?.Invoke();
        }

        public void Start(EndLevelType endLevelType)
        {
            SetupView(endLevelType);
            
            
            _view.MainButton
                .OnClickAsObservable()
                .Take(1)
                .Subscribe(_ => _model.MainButtonAction());
            
            _view.AdditionalButton
                .OnClickAsObservable()
                .Take(1)
                .Subscribe(_ => _model.AdditionalButtonAction());
            
            
            
            
            
            
        }

        private void SetupView(EndLevelType endLevelType)
        {
            switch (endLevelType)
            {
                case EndLevelType.WIN:
                    _view.SetResultText("WIN");
                    _view.SetMainButtonText("+100 by ADS");
                    _view.SetAdditionalButton("No thanks");
                    break;
                case EndLevelType.FAIL:
                    _view.SetResultText("FAIL");
                    _view.SetMainButtonText("+10 by ADS");
                    _view.SetAdditionalButton("No thanks");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(endLevelType), endLevelType, null);
            }
        }
    }
}