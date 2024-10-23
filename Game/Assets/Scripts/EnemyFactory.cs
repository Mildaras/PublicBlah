using UnityEngine;
using System;
using System.Reflection;

public static class EnemyFactory
{
    public static GameObject CreateEnemy(Type enemyType, Vector3 position, Quaternion rotation, GameObject[] waypoints)
    {
        string prefabPath = GetPrefabPathFromEnemyType(enemyType);
        if (string.IsNullOrEmpty(prefabPath))
        {
            return null;
        }

        GameObject enemyPrefab = Resources.Load<GameObject>(prefabPath);
        if (enemyPrefab == null)
        {
            return null;
        }

        GameObject newEnemy = GameObject.Instantiate(enemyPrefab, position, rotation);
        newEnemy.AddComponent(enemyType);

        InitializeEnemy(newEnemy, waypoints);
        return newEnemy;
    }

    private static string GetPrefabPathFromEnemyType(Type enemyType)
    {
        var propertyInfo = enemyType.GetProperty("PrefabPath", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

        if (propertyInfo != null)
        {
            Enemy enemyInstance = Activator.CreateInstance(enemyType) as Enemy;
            return propertyInfo.GetValue(enemyInstance) as string;
        }
        else
        {
            return null;
        }
    }

    private static void InitializeEnemy(GameObject enemy, GameObject[] waypoints)
    {
        MoveEnemy moveEnemy = enemy.GetComponent<MoveEnemy>();
        if (moveEnemy != null)
        {
            moveEnemy.waypoints = waypoints;
        }
    }
}
