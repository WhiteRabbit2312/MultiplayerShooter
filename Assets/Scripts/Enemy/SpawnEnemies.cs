using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SpawnEnemies : NetworkBehaviour
{
    [SerializeField] private Enemy[] enemyArray;
    [SerializeField] private Transform[] spawnEnemyPoint;
    [SerializeField] private Wave _wave;
    [SerializeField] private WaveManager _waveManager;

    private int _timer = 0;
    private IEnemyFactory _enemy;

    private IEnemyFactory _smallEnemy;
    private IEnemyFactory _bigEnemy;
    private IEnemyFactory _skeletonEnemy;

    private bool _canSpawn = false;

    public override void Spawned()
    {
        _smallEnemy = GetComponent<SmallEnemyFactory>();
        _bigEnemy = GetComponent<BigEnemyFactory>();
        _skeletonEnemy = GetComponent<SkeletonEnemyFactory>();

        GameManager.OnGameplay += CanPlay;
        GameManager.OnBreak += StopPlay;
    }

    public void CanPlay() => _canSpawn = true;
    public void StopPlay() => _canSpawn = false;

    public override void FixedUpdateNetwork()
    {
        if (Runner.IsServer && _canSpawn)
        {
            _timer++;
            if (_timer == 200)
            {
                //separate to another method SPAWN etc.
                int waveIdx = _waveManager.WaveCount;
                int randomEnemy = Random.Range(0, _wave.waveStat[waveIdx].Enemy);
                int randomSpawnPoint = Random.Range(0, spawnEnemyPoint.Length);

                switch (randomEnemy)
                {
                    case 0: _enemy = _smallEnemy; Debug.Log("Small enemy" + _enemy); break;
                    case 1: _enemy = _bigEnemy; Debug.Log("Big enemy" + _enemy); break;
                    case 2: _enemy = _skeletonEnemy; Debug.Log("Skeleton enemy" + _enemy); break;
                }

                _enemy.Initialize(enemyArray[randomEnemy], spawnEnemyPoint[randomSpawnPoint]);
                Debug.Log(" Initialize Enemy: " + _enemy);
                _enemy.SpawnEnemy();
                
                
                _timer = 0;
            }
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        Debug.Log("Despawned");

        GameManager.OnGameplay -= CanPlay;
        GameManager.OnBreak -= StopPlay;
    }
}
