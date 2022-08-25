using CodeBase.Infrastructure.Inputs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class HeroPresenter
    {
        private readonly HeroView _heroView;
        private readonly HeroModel _heroModel;

        public HeroPresenter(HeroView heroView, HeroModel heroModel)
        {
            _heroView = heroView;
            _heroModel = heroModel;

            Start();
        }

        void Start()
        {
            _heroView.FixedUpdateAsObservable()
                .Subscribe(_ => _heroModel.FixedUpdateHandle());

            _heroModel
                .Position
                .ObserveEveryValueChanged(z => z.Value)
                .Subscribe(pos => _heroView.Move(pos));
     
        }
        
    }
}