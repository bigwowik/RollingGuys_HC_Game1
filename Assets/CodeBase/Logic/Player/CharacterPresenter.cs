using CodeBase.Infrastructure.Inputs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class CharacterPresenter
    {
        public readonly CharacterView _characterView;
        private readonly ICharacterModel _characterModel;

        public CharacterPresenter(CharacterView characterView, ICharacterModel characterModel)
        {
            _characterView = characterView;
            _characterModel = characterModel;
        }


        public void Start()
        {
            _characterModel.SetGameObject(_characterView.gameObject);
            
            _characterView.FixedUpdateAsObservable()
                .TakeUntilDestroy(_characterView)
                .Subscribe(_ => _characterModel.FixedUpdateHandle());

            _characterModel
                .Position
                .ObserveEveryValueChanged(z => z.Value)
                .TakeUntilDestroy(_characterView)
                .Subscribe(pos => _characterView.Move(pos))
                ;
        }
        
    }

    public class HeroPresenter : CharacterPresenter
    {
        public HeroPresenter(CharacterView characterView, ICharacterModel characterModel) : base(characterView, characterModel)
        {
        }
    }
    
    public class FriendPresenter : CharacterPresenter
    {
        public FriendPresenter(CharacterView characterView, ICharacterModel characterModel) : base(characterView, characterModel)
        {
        }

        public class Factory : PlaceholderFactory<CharacterView, FriendPresenter>
        {
            
        }
    }
}