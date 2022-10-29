using System;
using System.Collections;
using System.Collections.Generic;
using ToolsAndMechanics.UserInterfaceManager;
using UnityEngine;
using Zenject;

public class EndGameController : MonoBehaviour
{
    [SerializeField]
    private WindowData loseWindow;

    [Inject]
    private Player player;
    [Inject]
    private WindowsController windowsController;

    private void Start()
    {
        player.HealthComponent.onDied += EndGame;
    }

    private void OnDestroy()
    {
        player.HealthComponent.onDied -= EndGame;
    }

    private void EndGame()
    {
        windowsController.SetWindow(loseWindow, true);
    }
}