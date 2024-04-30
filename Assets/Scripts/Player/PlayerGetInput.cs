using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerGetInput : NetworkBehaviour
{
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            Debug.Log("Can move");
            if (data.direction.sqrMagnitude > 0)
            {
                Debug.Log("Data is 0" + data.direction.sqrMagnitude);
                transform.position += data.direction * 0.1f;
            }
                

            else
            {
                Debug.Log("Data is 0" + data.direction.sqrMagnitude);
            }
        }
    }
}
