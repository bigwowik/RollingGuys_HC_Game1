using System;
using CodeBase.Helpers.Debug;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Settings;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Settings
{
    public class SettingsView : MonoBehaviour
    {
        public SettingsPresenter Presenter;
        
        public Button SoundsButton;
        public Button VibroButton;
        public Button InfoButton;
        
        public Button CloseButton;
        private IMediator _mediator;
        private ISettingsService _settingsService;

        [Inject]
        public void Construct(IMediator mediator, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _mediator = mediator;
        }
        
        private void Start()
        {
            Presenter = new SettingsPresenter(this, _mediator, _settingsService);
            Presenter.Start();
        }
    }
    
    

    public class SettingsModel
    {
        public ReactiveProperty<bool> IsSoundsEnable { get; set; }
        public ReactiveProperty<bool> IsVibroEnable { get; set; }

        public SettingsModel(bool isSoundEnable, bool isVibroEnable)
        {
            IsSoundsEnable = new ReactiveProperty<bool>(isSoundEnable);
            IsVibroEnable = new ReactiveProperty<bool>(isVibroEnable);
        }
    }

    public class SettingsPresenter
    {
        private SettingsView _settingsView;
        private readonly IMediator _mediator;
        private readonly ISettingsService _settingsService;
        private SettingsModel _settingsModel;

        public SettingsPresenter(SettingsView settingsView, IMediator mediator, ISettingsService settingsService)
        {
            _settingsView = settingsView;
            _mediator = mediator;
            _settingsService = settingsService;

            _settingsModel = new SettingsModel(true, true);
        }

        public void Start()
        {
            SubscribeOnView();

            SubscribeOnModel();
        }

        private void SubscribeOnModel()
        {
            _settingsModel
                .IsSoundsEnable
                .ObserveEveryValueChanged(z => z.Value)
                .Subscribe(_settingsService.SetSoundsEnable)
                .AddTo(_settingsView);

            _settingsModel
                .IsVibroEnable
                .ObserveEveryValueChanged(z => z.Value)
                .Subscribe(_settingsService.SetVibroEnable)
                .AddTo(_settingsView);
        }

        private void SubscribeOnView()
        {
            _settingsView
                .SoundsButton
                .OnClickAsObservable()
                .Subscribe(_ => _settingsModel.IsSoundsEnable.Value = !_settingsModel.IsSoundsEnable.Value)
                .AddTo(_settingsView);;

            _settingsView
                .VibroButton
                .OnClickAsObservable()
                .Subscribe(_ => _settingsModel.IsVibroEnable.Value = !_settingsModel.IsVibroEnable.Value)
                .AddTo(_settingsView);;

            _settingsView
                .InfoButton
                .OnClickAsObservable()
                .Subscribe(_ => Info())
                .AddTo(_settingsView);
            
            _settingsView
                .CloseButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _mediator.EnableSettings(false);
                    _mediator.EnableMainMenu(true);
                })
                .AddTo(_settingsView);
            
            
        }
        private void Info()
        {
            WDebug.Log("Show Info.", WType.UI);
        }

       
    }
}