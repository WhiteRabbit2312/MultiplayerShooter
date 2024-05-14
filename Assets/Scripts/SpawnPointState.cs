using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SpawnPointState : NetworkBehaviour
{
    private bool _spawnPointState;
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
            State = false;
        }
    }
}
