using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Enemy : NetworkBehaviour
{
    public int Damage;
    public int Health;
    [SerializeField] private float _speed;

    private List<Transform> _directionList = new List<Transform>();
    private Animator _enemyAnimator;

    private int _firstPlayer = 0;
    private int _secondPlayer = 1;
    private int _requieredPlayerAmount = 2;

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

        Vector2 playerPosition = NearestPlayer().position - transform.position;
        playerPosition.Normalize();

        
        transform.Translate(playerPosition * _speed * Runner.DeltaTime);
    }

    private Transform NearestPlayer()
    {
        List<float> listDistance = new List<float>();

        foreach (var item in _directionList)
        {
            float distance = Vector3.Distance(transform.position, item.position);
            listDistance.Add(distance);
        }

        if (_directionList.Count == _requieredPlayerAmount)
        {
            if (listDistance[_firstPlayer] < listDistance[_secondPlayer])
            {
                return _directionList[_firstPlayer];
            }
            else return _directionList[_secondPlayer];
        }

        else return _directionList[_firstPlayer];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            Debug.LogError("Bullet hit damage");
            _enemyAnimator.SetTrigger("Damage");
            Health -= bullet.Damage;
            PlayerStats stats = bullet.Init;
            stats.ChangeDamage(bullet.Damage);
            Runner.Despawn(bullet.Object);

            if (Health <= 0)
            {
                stats.ChangeKills();
                Runner.Despawn(Object);
            }
        }

        if(collision.tag == "Explosion")
        {
            Runner.Despawn(Object);
        }
    }

    private void DestroyEnemy()
    {
        Runner.Despawn(Object);
    }

    private void OnDestroy()
    {
        GameManager.OnBreak -= DestroyEnemy;
    }
}
