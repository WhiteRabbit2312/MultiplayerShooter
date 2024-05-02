using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Bomb : NetworkBehaviour, IItemEffect
{
    [SerializeField] private GameObject _explosion;

    public Transform SpawnPoint
    {
        get;
        set;
    }
    public void EnableEffect()
    {

        Debug.LogWarning("Explode");
        //Runner.Spawn(_explosion, SpawnPoint.position, Quaternion.identity);
        Runner.Despawn(Object);
    }
}
