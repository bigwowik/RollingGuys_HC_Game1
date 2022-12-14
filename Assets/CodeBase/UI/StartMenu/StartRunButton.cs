using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.UI.StartMenu
{
    public class StartRunButton : MonoBehaviour, IPointerDownHandler
    {
        [Inject] private IGameStateMachine GameStateMachine;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            GameStateMachine.Enter<GameLoopState>();
            gameObject.SetActive(false);
        }
    }
}