using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public event Action<int> onChangeScore = delegate {};

    public int Score => score;

    [SerializeField]
    private Transform leftPoint;
    [SerializeField]
    private Transform rightPoint;
    [SerializeField]
    private Transform downPoint;
    [SerializeField]
    private float yPos = -3.5f;
    [SerializeField]
    private int presetsOnStart = 4;
    [SerializeField]
    private float scrollingSpeed = 1f;

    [Space]
    [SerializeField]
    private int maxEasyScore = 5;
    [SerializeField]
    private int maxMediumScore = 10;
    [SerializeField]
    private LevelPresetsContainer easyContainer;
    [SerializeField]
    private LevelPresetsContainer mediumContainer;
    [SerializeField]
    private LevelPresetsContainer hardContainer;
    [SerializeField]
    private List<LevelPreset> presets;

    [Inject]
    private Player player;

    private LevelPreset lastPreset => presets[presets.Count - 1];

    private Coroutine gameCoroutine;
    private int score = 0;

    private void Start()
    {
        for (int i = 0; i < presetsOnStart; i++) SpawnPreset();

        InputController.onPointerDown += StartSpawn;
    }

    private void OnDestroy()
    {
        InputController.onPointerDown -= StartSpawn;
    }

    private void StartSpawn()
    {
        InputController.onPointerDown -= StartSpawn;
        player.HealthComponent.onDied += StopSpawn;
        gameCoroutine = StartCoroutine(GameCoroutine());
    }

    private void StopSpawn()
    {
        player.HealthComponent.onDied -= StopSpawn;
        StopCoroutine(gameCoroutine);
    }

    public Vector3 GetSidePosition(Vector3 playerPos, Vector3 innerSide, bool left)
    {
        Vector3 pos = left ? leftPoint.position : rightPoint.position;
        pos += playerPos - innerSide;
        pos.y = yPos;
        return pos;
    }

    private IEnumerator GameCoroutine()
    {
        while (enabled)
        {
            foreach (var preset in presets)
            {
                Vector3 pos = preset.transform.position;
                pos.y -= scrollingSpeed * Time.deltaTime;
                preset.transform.position = pos;
            }

            yield return null;

            if (presets[0].TopPosition.y < downPoint.position.y)
            {
                RemoveFirstPreset();
            }
        }
    }

    private void RemoveFirstPreset()
    {
        presets.RemoveAt(0);
        SpawnPreset();
        score++;
        onChangeScore(score);
    }

    private void SpawnPreset()
    {
        LevelPresetsContainer c = GetContainer();
        int rand = Random.Range(0, c.Presets.Count);
        LevelPreset preset = Instantiate(c.Presets[rand], GetSpawnPosition(c.Presets[rand]), Quaternion.identity);
        presets.Add(preset);
    }

    private LevelPresetsContainer GetContainer()
    {
        if (score < maxEasyScore) return easyContainer;
        else if (score < maxMediumScore) return mediumContainer;
        else return hardContainer;
    }

    private Vector3 GetSpawnPosition(LevelPreset preset)
    {
        return lastPreset.TopPosition + preset.GetOffset(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 pos = Vector3.zero;
        pos.y = yPos;
        Gizmos.DrawSphere(pos, 0.2f);
    }
}