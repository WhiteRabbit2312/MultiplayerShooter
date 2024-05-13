using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerHealth : NetworkBehaviour
{
    private Health _health;
    private Animator _animator;
    public override void Spawned()
    {
        _animator = GetComponentInChildren<Animator>();
        RPC_SendMessageHealth();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendMessageHealth()
    {
        if (!Object.HasStateAuthority)
            return;

        _health = gameObject.AddComponent<Health>();
        SetupHealthForEveryone();
    }

    private void SetupHealthForEveryone()
    {
        RPC_SendMessage();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendMessage()
    {
        _health = gameObject.AddComponent<Health>();
    }

    public bool GetDead()
    {
        bool isDead = _health.IsDead;
        return isDead;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("Player damaged");
        if (collision.TryGetComponent(out Enemy enemy) || collision.gameObject.tag == "SkeletonEnemy")
        {
            Debug.LogWarning("Player damaged try component");
            _health.PlayerDamaged(enemy.Damage);
            
        }
    }
}
