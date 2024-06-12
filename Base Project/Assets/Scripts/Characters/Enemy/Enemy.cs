using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : SpawnedItem, ITakingDamage
{
    public UnityAction<Enemy> EnemyDead;

    [SerializeField] private int _enemyHealthMax;
        
    private int _enemyHealth;

    public void TakeDamage(int damage)
    {
        _enemyHealth -= damage;
        if (_enemyHealth <= 0)
        {
            Death();
        }
    }

    protected void Death()
    {
        EnemyDead?.Invoke(this);
    }
}
