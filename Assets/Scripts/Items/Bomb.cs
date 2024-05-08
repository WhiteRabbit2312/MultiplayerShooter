using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Bomb : NetworkBehaviour, IItemEffect
{
    [SerializeField] private GameObject _explosion;

    public void EnableEffect(PlayerStats playerStats)
    {
        Debug.LogWarning("Explode");
        Runner.Spawn(_explosion, transform.position, Quaternion.identity);
        Runner.Despawn(Object);
    }
}
