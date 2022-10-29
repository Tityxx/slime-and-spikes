using System;
using System.Collections;
using System.Collections.Generic;
using ToolsAndMechanics.Utilities;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public event Action<float> onHealthChange = delegate { };
    public event Action onDied = delegate { };

    public float Health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = Math.Clamp(value, 0, MaxHealth);
            onHealthChange(currentHealth);
            if (currentHealth <= 0 && !isDead)
            {
                Died();
            }
        }
    }

    public float MaxHealth => health;
    public bool IsDead => isDead;

    [SerializeField]
    private bool needResetOnAwake = true;
    [SerializeField]
    private float health = 15f;
    [SerializeField, CustomReadOnly]
    private float currentHealth;

    private bool isDead = false;

    protected virtual void Awake()
    {
        if (needResetOnAwake)
            ResetHealth();
    }

    public virtual void ResetHealth(float h = -1f)
    {
        health = h == -1f ? health : h;
        Health = health;
        isDead = false;
    }

    protected virtual void Died()
    {
        isDead = true;
        onDied();
    }
}