using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SpawnPointState : NetworkBehaviour
{
    private bool _spawnPointState = false;

    public override void Spawned()
    {
        State = false;
    }

    public bool State
    {
        get
        {
            return _spawnPointState;
        }

        set
        {
            _spawnPointState = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.LogError("Player took item");
            _spawnPointState = false;
            State = _spawnPointState;
        }
    }
}
