using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField]
    private GameObject heartPrefab;

    [Inject]
    private Player player;

    private List<GameObject> hearts = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < player.HealthComponent.MaxHealth; i++)
        {
            var heart = Instantiate(heartPrefab, transform);
            hearts.Add(heart);
        }
    }

    private void OnEnable()
    {
        OnChangeHealth(player.HealthComponent.Health);
        player.HealthComponent.onHealthChange += OnChangeHealth;
    }

    private void OnDisable()
    {
        player.HealthComponent.onHealthChange -= OnChangeHealth;
    }

    private void OnChangeHealth(float health)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].SetActive(i < health);
        }
    }
}