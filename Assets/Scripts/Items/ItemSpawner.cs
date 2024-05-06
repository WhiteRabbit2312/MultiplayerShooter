using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ItemSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject[] _item;
    [SerializeField] private Transform[] _spawnPoint;
    private int _timer = 0;
    private int _itemCount = 3;

    public override void FixedUpdateNetwork()
    {
        _timer++;
        if (_timer == 200)
        {
            int randomItem = Random.Range(0, _itemCount);
            int randomSpawnPoint = Random.Range(0, _spawnPoint.Length);

            Runner.Spawn(_item[randomItem], _spawnPoint[randomSpawnPoint].position, Quaternion.identity);

            _timer = 0;
        }
    }
}
