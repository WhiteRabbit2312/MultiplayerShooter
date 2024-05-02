using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerTakeItem : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IItemEffect itemEffect))
        {
            itemEffect.EnableEffect();
        }
    }
}
