using CodeBase.Infrastructure.Inputs;
using CodeBase.Logic.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Friends
{
    public class FriendModel : ICharacterModel
    {
        public ReactiveProperty<Vector3> Position { get; private set; }

        private readonly FriendConfig _friendConfig;
        private readonly IInputService _inputService;

        private IFriend _friend;
        private Vector3 _lastNextFriendPosition;
        private GameObject _gameObjectModel;

        private bool _isActivated;


        public FriendModel(FriendConfig friendConfig, IInputService inputService)
        {
            _friendConfig = friendConfig;
            _inputService = inputService;

            Position = new ReactiveProperty<Vector3>();
        }

        public void SetGameObject(GameObject gameObjectModel)
        {
            _gameObjectModel = gameObjectModel;
            _friend = _gameObjectModel.GetComponent<IFriend>();

            Position.Value = _gameObjectModel.transform.position;
            
            FriendInteractive(_gameObjectModel);
        }

        public void FixedUpdateHandle()
        {
            if (!_isActivated) return;
            if (!_inputService.isActive) return;
            
            if (Vector3.Distance(_friend.NextFriend.Position, Position.Value) > _friendConfig.CriticalDistance)
                _lastNextFriendPosition = _friend.NextFriend.Position;

            Position.Value = Vector3.Lerp(Position.Value, _lastNextFriendPosition,
                _friendConfig.Speed * Time.fixedDeltaTime);
        }


        private void FriendInteractive(GameObject gameObject)
        {
            gameObject.OnTriggerEnterAsObservable()
                .Where(c => c.TryGetComponent<IFriend>(out var friend))
                .Take(1)
                .Subscribe(c => OnTriggerAction(c.gameObject));
        }

        private void OnTriggerAction(GameObject triggerObject)
        {
            _isActivated = true;
            triggerObject.GetComponent<IFriend>().AddFriend(_friend);
        }
    }
}