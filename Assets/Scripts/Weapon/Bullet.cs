using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using SecondTraineeGame;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _damage;
    private int _timer = 0;
    private Quaternion _dir;

    public void SetDirection(Quaternion dir)
    {
        _dir = dir;
    }

    public int Damage
    {
        get { return _damage; }
        private set
        {
            Damage = _damage;
        }
    }

    public override void FixedUpdateNetwork()
    {
        _timer++;
        if(_timer == _lifeTime)
        {
            Runner.Despawn(Object);
        }
        transform.rotation = _dir;

        transform.Translate(Vector2.right * _speed * Time.deltaTime, Space.Self);
    }
}
