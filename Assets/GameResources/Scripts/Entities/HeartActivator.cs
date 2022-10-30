using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

[Serializable]
public class HealChance
{
    public float Health;
    [Range(0f, 1f)]
    public float Chance;
}

public class HeartActivator : MonoBehaviour
{
    [SerializeField]
    private List<HealChance> chances;

    [Inject]
    private Player player;

    private void Start()
    {
        HealChance chance = GetChance();
        if (chance != null)
        {
            float rand = Random.Range(0f, 1f);
            gameObject.SetActive(rand <= chance.Chance);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private HealChance GetChance()
    {
        foreach (var c in chances)
            //FIXME: inject
            if (c.Health == player.HealthComponent.Health)
                return c;
        return null;
    }
}