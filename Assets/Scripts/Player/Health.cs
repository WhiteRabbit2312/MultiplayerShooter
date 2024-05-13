using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _hp = 15;
    public bool IsDead = false;



    public void PlayerDamaged(int damage)
    {
        Debug.LogWarning("Player got damage" + _hp);
        _hp -= damage;
        if (_hp <= 0)
        {
            _hp = 0;
            Debug.LogWarning("Dead");
            IsDead = true;
        }
    }
}
