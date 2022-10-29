using System;
using System.Collections;
using System.Collections.Generic;
using ToolsAndMechanics.UserInterfaceManager;
using UnityEngine;
using Zenject;

public class ScoreView : AbstractText
{
    [Inject]
    private LevelManager manager;

    private const string PREVIEW = "Score\n";

    private void OnEnable()
    {
        OnChangeScore(manager.Score);
        manager.onChangeScore += OnChangeScore;
    }

    private void OnDisable()
    {
        manager.onChangeScore -= OnChangeScore;
    }

    private void OnChangeScore(int score)
    {
        text.text = $"{PREVIEW}{score}";
    }
}