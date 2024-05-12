using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _hp = 15;
    public bool IsDead = false;



    public void PlayerDamaged(int damage)
    {
        _hp -= damage;
        if (_hp == 0)
        {
            IsDead = true;
        }
    }
}
