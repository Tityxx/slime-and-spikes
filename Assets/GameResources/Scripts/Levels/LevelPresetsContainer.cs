using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PresetsContainer", menuName = "Game/Levels/Presets Container")]
public class LevelPresetsContainer : ScriptableObject
{
    public List<LevelPreset> Presets => presets;

    [SerializeField]
    private List<LevelPreset> presets;
}