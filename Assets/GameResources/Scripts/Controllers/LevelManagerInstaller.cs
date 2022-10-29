using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManagerInstaller : MonoInstaller
{
    [SerializeField]
    private LevelManager manager;

    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromInstance(manager).AsSingle().NonLazy();
    }
}