using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Enemy : NetworkBehaviour
{
    public int Damage;
    public int Health;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layerMask;
    private Transform _direction;

    private List<Transform> _directionList = new List<Transform>();
    private Animator _enemyAnimator;
    private float _damagePerTime = 60f;
    private bool _causeDamage = false;
    private PlayerStats _playerStats;//TODO: Initialize player

    public void Init(List<Transform> transformList)
    {
        _directionList = transformList;
    }

    public override void Spawned()
    {
        if (!HasStateAuthority) return;
        _enemyAnimator = GetComponentInChildren<Animator>();
        GameManager.OnBreak += DestroyEnemy;
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
        {
            Debug.Log("HasStateAuthority");
            return;
        }

        foreach(var item in _directionList)
        {
            float distance = Vector3.Distance(transform.position, item.position);
        }

        Vector2 playerPosition = _direction.position - transform.position;
        playerPosition.Normalize();

        transform.Translate(playerPosition * _speed * Runner.DeltaTime);

        if (_causeDamage)
        {
            if (_damagePerTime >= 60)
            {
                _damagePerTime = 0;

                Debug.LogWarning("Player get damage");

                if (_playerStats != null)
                {
                    _playerStats.GetDamage(Damage);
                }
            }
            _damagePerTime++;
        }
    }

    private Transform NearestPlayer()
    {
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            _enemyAnimator.SetTrigger("Damage");
            Health -= bullet.Damage;
            PlayerStats.OnDamage.Invoke(bullet.Damage);

            Runner.Despawn(bullet.Object);

            if (Health <= 0)
            {
                PlayerStats.OnKill?.Invoke();
                Runner.Despawn(Object);
            }
        }

        if (collision.TryGetComponent(out PlayerStats playerStats))
        {
            
            _playerStats = playerStats;
            _causeDamage = true;
        }
    }

    private void DestroyEnemy()
    {
        Runner.Despawn(Object);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _causeDamage = false;
        }
    }
}
