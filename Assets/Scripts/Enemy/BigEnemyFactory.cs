using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BigEnemyFactory : NetworkBehaviour, IEnemyFactory
{
    private Enemy _spawnedEnemy;
    private Vector2 _spawnPosition = new Vector2(-3, 3);
    private BasicSpawner _basicSpawner;

    public void Initialize(Enemy spawnedEnemy)
    {
        _spawnedEnemy = spawnedEnemy;
    }


    //TODO: не використовувати networkObject для спавну
    public void SpawnEnemy()
    {
        Enemy enemySpawned = Runner.Spawn(_spawnedEnemy, _spawnPosition, Quaternion.identity);
        _basicSpawner = GameObject.FindObjectOfType<BasicSpawner>();
        enemySpawned.Init(_basicSpawner.CharacterPosition);
    }
}
