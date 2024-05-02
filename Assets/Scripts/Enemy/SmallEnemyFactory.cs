using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SmallEnemyFactory : NetworkBehaviour, IEnemyFactory
{
    private Enemy _spawnedEnemy;
    private NetworkObject _networkObject;
    private Vector2 _spawnPosition = new Vector2(-1, 1);

    public void Initialize(Enemy spawnedEnemy)
    {
        _spawnedEnemy = spawnedEnemy;
        _networkObject = _spawnedEnemy.GetComponent<NetworkObject>();
    }
    //TODO: не використовувати networkObject для спавну

    public NetworkObject SpawnEnemy()
    {
        Debug.Log("Small enemy network obj" + _networkObject);
        return Runner.Spawn(_networkObject, _spawnPosition, Quaternion.identity);
    }
}
