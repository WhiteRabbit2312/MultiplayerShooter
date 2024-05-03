using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SmallEnemyFactory : NetworkBehaviour, IEnemyFactory
{
    private Enemy _spawnedEnemy;
    private Vector2 _spawnPosition = new Vector2(-1, 1);
    private BasicSpawner _basicSpawner;

    public void Initialize(Enemy spawnedEnemy)
    {
        _spawnedEnemy = spawnedEnemy;
    }
    //TODO: не використовувати networkObject для спавну

    public void SpawnEnemy()
    {
        Enemy spawnedEnemy = Runner.Spawn(_spawnedEnemy, _spawnPosition, Quaternion.identity);
        _basicSpawner = GameObject.FindObjectOfType<BasicSpawner>();
        spawnedEnemy.Init(_basicSpawner.CharacterPosition);
    }
}
