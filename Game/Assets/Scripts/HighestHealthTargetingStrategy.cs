using System.Collections.Generic;
using UnityEngine;

public class HighestHealthTargetingStrategy : ITargetingStrategy
{
    public GameObject SelectTarget(List<GameObject> enemiesInRange)
    {
        GameObject target = null;
        float highestHealth = float.MinValue;

        foreach (GameObject enemy in enemiesInRange)
        {
            Transform healthBarTransform = enemy.transform.Find("HealthBar");
            if (healthBarTransform != null)
            {
                HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
                if (healthBar != null) 
                {
                    float enemyHealth = healthBar.currentHealth;
                    if (enemyHealth > highestHealth)
                    {
                        target = enemy;
                        highestHealth = enemyHealth;
                    }
                }
            }
        }

        return target;
    }
}
