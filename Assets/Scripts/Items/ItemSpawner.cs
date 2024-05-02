using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ItemSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject[] _item;
    private int _timer = 0;
    private int _itemCount = 3;

    public override void FixedUpdateNetwork()
    {
        _timer++;
        if (_timer == 200)
        {
            int randomItem = Random.Range(0, _itemCount);

            int _randomX = Random.Range(-5, 5);
            Runner.Spawn(_item[randomItem], new Vector3(_randomX, 0, 0), Quaternion.identity);

            _timer = 0;
        }
    }
}
