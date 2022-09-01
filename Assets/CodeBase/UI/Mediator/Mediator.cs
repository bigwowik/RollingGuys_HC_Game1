using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using CodeBase.Infrastructure.States;
using EasyButtons;
using TMPro;
using UnityEngine;
using Zenject;
using Color = System.Drawing.Color;

public class Mediator : MonoBehaviour, IMediator
{
    [SerializeField] private Transform MainMenuButtons;
    [SerializeField] private TextMeshProUGUI LevelProgressText;
    [SerializeField] private TextMeshProUGUI CoinsText;
    [SerializeField] private Transform Settings;
    [SerializeField] private Transform MainMenu;
    

    [Button()] public void DisableMainMenuButtons() => MainMenuButtons.gameObject.SetActive(false);
    public void SetLevelText(string text) => LevelProgressText.text = text;
    public void SetCoinsText(string text) => CoinsText.text = text;
    public void EnableSettings(bool enable) => Settings.gameObject.SetActive(enable);
    public void EnableMainMenu(bool enable) => MainMenu.gameObject.SetActive(enable);
}