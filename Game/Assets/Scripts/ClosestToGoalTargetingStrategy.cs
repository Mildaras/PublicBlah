using System.Collections.Generic;
using UnityEngine;

public class ClosestToGoalTargetingStrategy : ITargetingStrategy
{
    public GameObject SelectTarget(List<GameObject> enemiesInRange)
    {
        GameObject target = null;
        float minimalDistance = float.MaxValue;

        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
            if (distanceToGoal < minimalDistance)
            {
                target = enemy;
                minimalDistance = distanceToGoal;
            }
        }

        return target;
    }
}
