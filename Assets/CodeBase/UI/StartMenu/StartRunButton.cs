using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.UI.StartMenu
{
    public class StartRunButton : MonoBehaviour, IPointerDownHandler
    {
        [Inject] private IMediator Mediator;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Mediator.StartRunMode();
            gameObject.SetActive(false);
        }
    }
}