using System.Collections.Generic;
using UnityEngine;

public class NearestEnemyTargetingStrategy : ITargetingStrategy
{
    private Transform towerTransform;

    public NearestEnemyTargetingStrategy(Transform towerTransform)
    {
        this.towerTransform = towerTransform;
    }

    public GameObject SelectTarget(List<GameObject> enemiesInRange)
    {
        GameObject target = null;
        float minimalDistance = float.MaxValue;

        foreach (GameObject enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(enemy.transform.position, towerTransform.position);
            if (distance < minimalDistance)
            {
                target = enemy;
                minimalDistance = distance;
            }
        }

        return target;
    }
}
