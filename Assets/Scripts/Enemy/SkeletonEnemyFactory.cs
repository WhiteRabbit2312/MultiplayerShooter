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

    public void SpawnedEnemy()
    {
        Debug.Log("Skeleton enemy network obj" + _networkObject);
        if (Runner == null)
            Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA!!!!!");
        Runner.Spawn(_networkObject, _spawnPosition, Quaternion.identity);
    }
}
