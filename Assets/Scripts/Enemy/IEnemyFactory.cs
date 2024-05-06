using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public interface IEnemyFactory 
{
    public void SpawnEnemy(); //SpawnEnemy
    public void Initialize(Enemy enemy, Transform spawnPoint);
}
