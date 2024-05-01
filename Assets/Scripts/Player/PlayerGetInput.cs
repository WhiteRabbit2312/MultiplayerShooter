using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerGetInput : NetworkBehaviour
{
    [SerializeField] private float _speed = 5f;
    //transform.Translate(moveDirection * _speed * Time.deltaTime);
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            transform.Translate(data.direction * _speed);
        }
    }
}
