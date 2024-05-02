using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BigEnemyFactory : NetworkBehaviour, IEnemyFactory
{
    private Enemy _spawnedEnemy;
    private NetworkObject _networkObject;
    private Vector2 _spawnPosition = new Vector2(-3, 3);

    public void Initialize(Enemy spawnedEnemy)
    {
        _spawnedEnemy = spawnedEnemy;
        _networkObject = _spawnedEnemy.GetComponent<NetworkObject>();
    }


    //TODO: не використовувати networkObject для спавну
    public NetworkObject SpawnEnemy()
    {
        Debug.Log("Big enemy network obj" + _networkObject);
        return Runner.Spawn(_networkObject, _spawnPosition, Quaternion.identity);
    }
}
