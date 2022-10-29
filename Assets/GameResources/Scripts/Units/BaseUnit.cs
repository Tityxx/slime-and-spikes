using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour, IAttackable
{
    public HealthComponent HealthComponent => healthComponent;

    [SerializeField]
    protected HealthComponent healthComponent;

    public virtual void TakeDamage(float damage = 1)
    {
        healthComponent.Health -= damage;
        if (healthComponent.Health <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(float heal = 1)
    {
        healthComponent.Health += heal;
    }

    public bool IsAlive()
    {
        return !healthComponent.IsDead;
    }

    protected virtual void Die() { }
}