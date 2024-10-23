using UnityEngine;

public static class EnemyFactory
{
    public static GameObject CreateEnemy(string prefabPath, Vector3 position, Quaternion rotation, GameObject[] waypoints)
    {
        GameObject enemyPrefab = Resources.Load<GameObject>(prefabPath);
        GameObject newEnemy = GameObject.Instantiate(enemyPrefab, position, rotation);
        InitializeEnemy(newEnemy, waypoints, prefabPath);
        return newEnemy;
    }

    private static void InitializeEnemy(GameObject enemy, GameObject[] waypoints, string prefabPath)
    {
        MoveEnemy moveEnemy = enemy.GetComponent<MoveEnemy>();
        if (moveEnemy != null)
        {
            moveEnemy.waypoints = waypoints;
            if (prefabPath.Contains("Enemy2"))
            {
                moveEnemy.speed = 1.5f;
            }
            else
            {
                moveEnemy.speed = 2.5f;
            }
        }

        HealthBar healthBar = enemy.GetComponent<HealthBar>();
        if (healthBar != null)
        {
            if (prefabPath.Contains("Enemy 2"))
            {
                healthBar.maxHealth = 150;
                healthBar.currentHealth = 150;
            }
            else
            {
                healthBar.maxHealth = 100;
                healthBar.currentHealth = 100;
            }
        }
    }
}
