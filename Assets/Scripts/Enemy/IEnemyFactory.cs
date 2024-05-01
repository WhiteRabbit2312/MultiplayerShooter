using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public interface IEnemyFactory 
{
    public void SpawnedEnemy();
    public void Initialize(Enemy enemy);
}
