using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Enemy : NetworkBehaviour
{
    public int Damage;
    public int Health;
    [SerializeField] private float _speed;
    private Transform _direction;
    private Animator _enemyAnimator;
    private float _sphereRadius = 5f;
    private float _damagePerTime = 60f;

    public void Init(List<Transform> transformList)
    {
        _direction = transformList[0];
    }

    public override void Spawned()
    {
        if (!HasStateAuthority) return;
        _enemyAnimator = GetComponentInChildren<Animator>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
        {
            Debug.Log("HasStateAuthority");
            return;
        }

        Vector2 playerPosition = _direction.position - transform.position;
        playerPosition.Normalize();

        transform.Translate(playerPosition * _speed * Runner.DeltaTime);
        

        Collider[] colliders = Physics.OverlapSphere(transform.position, _sphereRadius);

        if (colliders != null)
        {
            if (_damagePerTime == 60)
            {
                _damagePerTime = 0;
                foreach (Collider collider in colliders)
                {
                    if (collider.isTrigger)
                    {
                        EnemyHitPlayer(collider);
                    }
                }
            }

            _damagePerTime++;
        }
    }

    private void EnemyHitPlayer(Collider collider)
    {
        if(collider.TryGetComponent(out PlayerStats playerStats))
        {
            playerStats.GetDamage(Damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            
            _enemyAnimator.SetTrigger("Damage");
            Health -= bullet.Damage;

            Runner.Despawn(bullet.Object);

            if (Health <= 0)
            {
                Debug.LogWarning("enemy damaged");
                Runner.Despawn(Object);
            }
        }
    }
}
