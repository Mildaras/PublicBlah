using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract string PrefabPath { get; }
    public float speed;
    public int maxHealth;
    public int currentHealth;

    protected virtual void Awake()
    {
        InitializeComponents();
    }

    protected void InitializeComponents()
    {
        MoveEnemy moveEnemy = GetComponent<MoveEnemy>();
        if (moveEnemy != null)
        {
            moveEnemy.speed = speed;
        }

        HealthBar healthBar = GetComponent<HealthBar>();
        if (healthBar != null)
        {
            healthBar.maxHealth = maxHealth;
            healthBar.currentHealth = currentHealth;
        }
    }
}
