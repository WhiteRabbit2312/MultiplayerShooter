using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerDamage : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Debug.LogWarning("In collision");
            PlayerStatChanged.OnHPChanged?.Invoke(enemy.Damage);
            //PlayerAnimationManager.OnPlayerDamage?.Invoke();
        }
    }
}
