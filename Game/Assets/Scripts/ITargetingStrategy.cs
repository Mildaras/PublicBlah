using System.Collections.Generic;
using UnityEngine;

public interface ITargetingStrategy
{
    GameObject SelectTarget(List<GameObject> enemiesInRange);
}
