using UnityEngine;

public class Enemy3 : Enemy
{
    public override string PrefabPath => "Prefabs/Enemy 3";

    protected override void Awake()
    {
        speed = 2f;
        maxHealth = 120;
        currentHealth = 120;

        base.Awake();
    }
}
