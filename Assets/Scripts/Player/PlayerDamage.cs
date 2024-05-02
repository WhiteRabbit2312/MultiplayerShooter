using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerDamage : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SimpleEnemy" || collision.tag == "BigEnemy" || collision.tag == "SkeletonEnemy")
        {
            Debug.LogWarning("Damage");
        }
    }
}
