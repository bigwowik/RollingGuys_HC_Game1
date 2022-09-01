using Zenject;

namespace CodeBase.UI.Buttons
{
    public class SettingsButton : ButtonBase
    {
        private IMediator _mediator;

        [Inject]
        public void Construct(IMediator mediator)
        {
            _mediator = mediator;
        }
        protected override void OnButtonClickHandler()
        {
            _mediator.EnableSettings(true);
            _mediator.EnableMainMenu(false);
        }
    }
}