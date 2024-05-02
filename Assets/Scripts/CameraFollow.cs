using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CameraFollow : NetworkBehaviour
{
    public override void Spawned()
    {

        gameObject.SetActive(HasInputAuthority);

    }
}
