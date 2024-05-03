using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using SecondTraineeGame;

public class Bullet : NetworkBehaviour
{
    private float _speed = 1f;

    public override void FixedUpdateNetwork()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    
}
