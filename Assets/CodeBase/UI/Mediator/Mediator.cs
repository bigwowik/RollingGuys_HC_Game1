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
    [SerializeField] private Transform LevelProgressButton;
    [SerializeField] private TextMeshProUGUI LevelProgressText;
    [SerializeField] private TextMeshProUGUI CoinsText;

    [Button()] public void DisableMainMenu() => MainMenuButtons.gameObject.SetActive(false);
    public void SetLevelText(string text) => LevelProgressText.text = text;
    public void SetCoinsText(string text) => CoinsText.text = text;
}