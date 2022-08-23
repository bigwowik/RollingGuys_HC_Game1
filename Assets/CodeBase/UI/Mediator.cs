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
    [SerializeField] private Transform MainMenuButtons;
    [SerializeField] private Transform LevelProgressButton;

    [Button()] public void DisableMainMenu() => MainMenuButtons.gameObject.SetActive(false); 
    [Button()] public void EnableLevelProgressBar() => LevelProgressButton.gameObject.SetActive(true); 
}

public interface IMediator
{
    void DisableMainMenu();
}
