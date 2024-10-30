using UnityEngine;

public class FastEnemy : Enemy
{
    public override string PrefabPath => "Prefabs/Enemy 2";

    protected override void Awake()
    {
        speed = 2.5f;
        maxHealth = 110;
        currentHealth = 110;

        base.Awake();
    }
}
