using UnityEngine;

public class Enemy2 : Enemy
{
    public override string PrefabPath => "Prefabs/Enemy 2";

    protected override void Awake()
    {
        speed = 2.5f;
        maxHealth = 150;
        currentHealth = 150;

        base.Awake();
    }
}
