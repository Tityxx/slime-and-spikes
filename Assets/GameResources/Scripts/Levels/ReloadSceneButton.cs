using System;
using System.Collections;
using System.Collections.Generic;
using ToolsAndMechanics.UserInterfaceManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneButton : AbstractButton
{
    public override void OnButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
