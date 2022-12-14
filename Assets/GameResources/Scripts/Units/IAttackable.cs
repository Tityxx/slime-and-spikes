using UnityEngine;

public interface IAttackable
{
    public void TakeDamage(float damage);
    public void Heal(float heal);
    public bool IsAlive();
}