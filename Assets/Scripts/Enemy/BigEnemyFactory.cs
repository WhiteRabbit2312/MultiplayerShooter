using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BigEnemyFactory : NetworkBehaviour, IEnemyFactory
{
    private Enemy _spawnedEnemy;
    private Vector2 _spawnPosition;
    private BasicSpawner _basicSpawner;

    public void Initialize(Enemy spawnedEnemy, Transform spawnPosition)
    {
        _spawnedEnemy = spawnedEnemy;
        _spawnPosition = spawnPosition.position;
        _basicSpawner = FindObjectOfType<BasicSpawner>();
    }


    //TODO: не використовувати networkObject для спавну
    public void SpawnEnemy()
    {
        Enemy enemySpawned = Runner.Spawn(_spawnedEnemy, _spawnPosition, Quaternion.identity);
        
        enemySpawned.Init(_basicSpawner.CharacterPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
