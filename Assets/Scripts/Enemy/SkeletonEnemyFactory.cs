using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SkeletonEnemyFactory : NetworkBehaviour, IEnemyFactory
{
    private Enemy _spawnedEnemy;
    private Vector2 _spawnPosition;
    private BasicSpawner _basicSpawner;

    public void Initialize(Enemy spawnedEnemy, Transform spawnPosition)
    {
        _spawnedEnemy = spawnedEnemy;
        _spawnPosition = spawnPosition.position;
    }
    //TODO: не використовувати networkObject для спавну

    public void SpawnEnemy()
    {
        Enemy enemySpawned = Runner.Spawn(_spawnedEnemy, _spawnPosition, Quaternion.identity);
        _basicSpawner = GameObject.FindObjectOfType<BasicSpawner>();
        enemySpawned.Init(_basicSpawner.CharacterPosition);

    }
}
