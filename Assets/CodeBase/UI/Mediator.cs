using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using CodeBase.Infrastructure.States;
using EasyButtons;
using UnityEngine;
using Zenject;
using Color = System.Drawing.Color;

public class Mediator : MonoBehaviour, IMediator
{
    [Inject] private IGameStateMachine GameStateMachine;

    [SerializeField] private Transform MainMenuButtons;

    //[Button] public void StartRunMode() => GameStateMachine.Enter<GameLoopState>();

    [Button()] public void DisableMainMenu()
    {
        //MainMenuButtons.gameObject.SetActive(false);
    }
    
    [Button()] public void DisableMainMenu2()
    {
        //MainMenuButtons.gameObject.SetActive(false);
    }
}

public interface IMediator
{
    void DisableMainMenu();
}
