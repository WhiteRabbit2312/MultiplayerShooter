using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerDamage : NetworkBehaviour
{
    private int _damagePerTime = 59;
    private int _damage;
    private bool _causeDamage = false;

    public override void FixedUpdateNetwork()
    {
        if (_causeDamage)
        {
            if (_damagePerTime == 60)
            {
                PlayerStatChanged.OnHPChanged?.Invoke(_damage);
                _damagePerTime = 0;
            }

            _damagePerTime++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Debug.LogWarning("In collision");

            _damage = enemy.Damage;
            _causeDamage = true;

            //PlayerAnimationManager.OnPlayerDamage?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _causeDamage = false;
        }
    }

}
