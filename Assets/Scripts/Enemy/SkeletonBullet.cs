using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SkeletonBullet : NetworkBehaviour
{
    private Vector3 _direction;
    private int _lifeTime = 800;
    private int _timer = 0;

    public void Init(Transform transform)
    {
        if(!transform.gameObject.GetComponent<PlayerStats>().Dead)
            _direction = transform.position;
    }

    public override void FixedUpdateNetwork()
    {
        Vector3 bulletDirection = _direction - transform.position;
        bulletDirection.Normalize();
        transform.Translate(bulletDirection * Runner.DeltaTime * 10f);

        if (_timer == _lifeTime)
        {
            _timer = 0;
            Runner.Despawn(Object);
        }
        _timer++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Runner.Despawn(Object);
        }
    }
}
