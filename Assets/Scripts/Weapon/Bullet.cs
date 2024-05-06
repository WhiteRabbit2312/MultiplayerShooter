using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using SecondTraineeGame;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float _speed;
    private int _timer = 0;
    private int _lifeTime;
    private Quaternion _dir;

    public void SetRange(int lifeTime)
    {
        _lifeTime = lifeTime;
    }

    public void SetDirection(Quaternion dir)
    {
        _dir = dir;
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
