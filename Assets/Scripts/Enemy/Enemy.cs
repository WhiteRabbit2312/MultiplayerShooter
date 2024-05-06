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
    private float t = 0f;

    public void Init(List<Transform> transformList)
    {
        _direction = transformList[0];
    }

    public override void Spawned()
    {
        if (!HasStateAuthority) return;
    }

    public override void FixedUpdateNetwork()
    {
        Debug.LogWarning("Fixed update enemy");

        if (!HasStateAuthority)
        {
            Debug.Log("HasStateAuthority");
            return;
        }

        t = Runner.DeltaTime * _speed;
        transform.position = Vector3.Lerp(transform.position, _direction.position, t);
        if(transform.position == _direction.position)
        {
            t = 0f;
        }

        if(Health <= 0)
        {
            Runner.Despawn(Object);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            Health -= bullet.Damage;
        }
    }

    /*
    public void OnEnemyMove(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkEnemyData();
        data.direction = transform.position;
        input.Set(data);
    }*/
}
