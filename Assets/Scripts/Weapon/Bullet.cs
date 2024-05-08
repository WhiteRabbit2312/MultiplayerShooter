using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using SecondTraineeGame;

public class Bullet : NetworkBehaviour
{
    public float Speed;
    public float LifeTime;
    public int Damage;
    private int _timer = 0;
    private Quaternion _dir;


    public void SetDirection(Quaternion dir)
    {
        _dir = dir;
    }


    public override void FixedUpdateNetwork()
    {
        if(_timer == LifeTime)
        {
            Runner.Despawn(Object);
        }

        _timer++;

        transform.rotation = _dir;

        transform.Translate(Vector2.right * Speed * Time.deltaTime, Space.Self);
    }
}
