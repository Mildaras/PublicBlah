using UnityEngine;

public class Enemy1 : Enemy
{
    public override string PrefabPath => "Prefabs/Enemy";

    protected override void Awake()
    {
        speed = 1.5f;
        maxHealth = 100;
        currentHealth = 100;

        base.Awake();
    }
}
