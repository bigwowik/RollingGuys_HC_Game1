using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

public class Mediator : MonoBehaviour, IMediator
{
    [Inject] private IGameStateMachine GameStateMachine;

    [SerializeField] private Transform MainMenuButtons;

    public void StartRunMode() => GameStateMachine.Enter<GameLoopState>();

    public void DisableMainMenu() => MainMenuButtons.gameObject.SetActive(false);
}

public interface IMediator
{
    void StartRunMode();
    void DisableMainMenu();
}
