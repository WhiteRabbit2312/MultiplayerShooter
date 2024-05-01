using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SpawnEnemies : NetworkBehaviour
{
    [SerializeField] private Enemy[] enemyArray;

    private float _timer = 0;
    private IEnemyFactory _enemy;
    private int _enemyNumber = 3;

    private IEnemyFactory _smallEnemy;
    private IEnemyFactory _bigEnemy;
    private IEnemyFactory _skeletonEnemy;

    public override void Spawned()
    {
        _smallEnemy = GetComponent<SmallEnemyFactory>();
        _bigEnemy = GetComponent<BigEnemyFactory>();
        _skeletonEnemy = GetComponent<SkeletonEnemyFactory>();
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.IsServer)
        {
            _timer++;
            if (_timer == 60)
            {
                int randomEnemy = Random.Range(0, _enemyNumber);

                switch (randomEnemy)
                {
                    case 0: _enemy = _smallEnemy; Debug.Log("Small enemy" + _enemy); break;
                    case 1: _enemy = _bigEnemy; Debug.Log("Big enemy" + _enemy); break;
                    case 2: _enemy = _skeletonEnemy; Debug.Log("Skeleton enemy" + _enemy); break;
                }

                _enemy.Initialize(enemyArray[randomEnemy]);
                Debug.Log(" Initialize Enemy: " + _enemy);
                _enemy.SpawnedEnemy();
                
                _timer = 0;
            }
        }
    }
}
