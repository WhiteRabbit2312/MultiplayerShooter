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
            Debug.Log("Can move");
            if(true)//if (data.direction.sqrMagnitude > 0)
            {
                Debug.Log("Data is 0" + data.direction.sqrMagnitude);
                transform.Translate(data.direction * _speed);
                //transform.position += data.direction * 0.1f;
            }
                

            else
            {
                Debug.Log("Data is 0" + data.direction.sqrMagnitude);
            }
        }
    }
}
