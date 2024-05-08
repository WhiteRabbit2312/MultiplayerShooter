using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerDamage : NetworkBehaviour
{
    private int _damagePerTime = 59;
    private int _damage;
    private bool _causeDamage = false;

    private bool _isDead = false;
    public override void Spawned()
    {
        GameManager.OnDeath += Dead;
    }

    public void Dead() => _isDead = true;

    public override void FixedUpdateNetwork()
    {
        
        if (_causeDamage && !_isDead)
        {
            if (_damagePerTime == 60)
            {
                PlayerStatChanged.OnHPChanged?.Invoke(-1);
                //PlayerAnimationManager.OnPlayerDamage?.Invoke();
                _damagePerTime = 0;

                
            }

            _damagePerTime++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _damage = enemy.Damage;
            _causeDamage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _causeDamage = false;
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        GameManager.OnDeath -= Dead;
    }
}
