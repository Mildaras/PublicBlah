using System.Collections.Generic;
using UnityEngine;

public class LowestHealthTargetingStrategy : ITargetingStrategy
{
    public GameObject SelectTarget(List<GameObject> enemiesInRange)
    {
        GameObject target = null;
        float lowestHealth = float.MaxValue;

        foreach (GameObject enemy in enemiesInRange)
        {
            Transform healthBarTransform = enemy.transform.Find("HealthBar");
            if (healthBarTransform != null)
            {
                HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
                if (healthBar != null) 
                {
                    float enemyHealth = healthBar.currentHealth;
                    if (enemyHealth < lowestHealth)
                    {
                        target = enemy;
                        lowestHealth = enemyHealth;
                    }
                }
            }
        }

        return target;
    }
}
