using UnityEngine;

public class StrongEnemy : Enemy
{
    public override string PrefabPath => "Prefabs/Enemy 3";

    protected override void Awake()
    {
        speed = 2f;
        maxHealth = 150;
        currentHealth = 150;

        base.Awake();
    }
}
