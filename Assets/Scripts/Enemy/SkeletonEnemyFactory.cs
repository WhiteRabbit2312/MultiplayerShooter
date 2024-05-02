using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SkeletonEnemyFactory : NetworkBehaviour, IEnemyFactory
{
    private Enemy _spawnedEnemy;
    private NetworkObject _networkObject;
    private Vector2 _spawnPosition = new Vector2(-2, 2);

    public void Initialize(Enemy spawnedEnemy)
    {
        _spawnedEnemy = spawnedEnemy;
        _networkObject = _spawnedEnemy.GetComponent<NetworkObject>();
    }
    //TODO: не використовувати networkObject для спавну

    public NetworkObject SpawnEnemy()
    {
        Debug.Log("Skeleton enemy network obj" + _networkObject);
        if (Runner == null)
            Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA!!!!!");
        return Runner.Spawn(_networkObject, _spawnPosition, Quaternion.identity);
    }
}
