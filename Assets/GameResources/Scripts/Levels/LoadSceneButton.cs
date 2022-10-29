using System;
using System.Collections;
using System.Collections.Generic;
using ToolsAndMechanics.UserInterfaceManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : AbstractButton
{
    [SerializeField]
    private string sceneName;

    public override void OnButtonClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
