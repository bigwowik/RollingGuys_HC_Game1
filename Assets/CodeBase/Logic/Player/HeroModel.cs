using System;
using CodeBase.Infrastructure.Inputs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class HeroModel : ICharacterModel
    {
        public ReactiveProperty<Vector3> Position { get; private set; }

        private readonly HeroConfig _heroConfig;
        private readonly IInputService _inputService;
        private GameObject _gameObjectModel;

        public HeroModel(HeroConfig heroConfig, IInputService inputService)
        {
            _heroConfig = heroConfig;
            _inputService = inputService;

            Position = new ReactiveProperty<Vector3>();
        }

        public void FixedUpdateHandle()
        {
            if (_inputService.isActive)
            {
                Position.Value +=
                    (Vector3.forward * _heroConfig.ForwardSpeed
                     + Vector3.right * _heroConfig.HorizontalSpeed * _inputService.VelocityX)
                    * Time.fixedDeltaTime;
            }
        }

        public void SetGameObject(GameObject gameObjectModel)
        {
            _gameObjectModel = gameObjectModel;
        }
    }
}