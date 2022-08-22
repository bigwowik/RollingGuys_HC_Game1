using CodeBase.Infrastructure.Inputs;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class HeroPresenter
    {
        private IHeroModel _heroModel;
        private IHeroView _heroView;
        private readonly IInputService _inputService;

        public HeroPresenter(IHeroModel heroModel, IHeroView heroView, IInputService inputService)
        {
            _heroModel = heroModel;
            _heroView = heroView;
            _inputService = inputService;

            _heroView.InputUpdateEvent += InputUpdateHandle;
            _heroModel.UpdatePosition += UpdatePosition;

        }

        private void InputUpdateHandle()
        {
            var velocityX = _inputService.VelocityX;
            var isInputActive = _inputService.isActive;

            _heroModel.Move(velocityX);
        }

        void UpdatePosition(Vector3 newPosition)
        {
            _heroView.Move(newPosition);
        }
        
    }
}