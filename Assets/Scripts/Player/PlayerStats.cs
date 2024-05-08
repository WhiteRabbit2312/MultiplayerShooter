using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class PlayerStats : NetworkBehaviour
{
    private int _hp = 15;
    private int _ammo = 1000;
    private int _kills = 0;

    private const int FullHP = 15;
    private const int FullAmmo = 1000;

    public override void Spawned()
    {
        
    }

    public void GetDamage(int change)
    {
        if (_hp > 0)
        {
            _hp -= change;
            Debug.LogWarning("HP " + _hp);
        }

        else
        {
            _hp = 0;
            Debug.LogWarning("Dead");
            //PlayerAnimationManager.OnPlayerDeath?.Invoke();
            GameManager.Death();
        }
    }

    public void TakeAidKit()
    {
        _hp = FullHP;
    }


    public void UseAmmo()
    {
        if (_ammo > 0)
        {
            _ammo--;
        }

        else
        {
            _ammo = 0;
        }
    }

    public bool HaveAmmo()
    {
        return _ammo != 0 ? true : false;
    }

    public void TakeAmmoBox()
    {
        _ammo = FullAmmo;
    }

    public void ChangeKills(int change)
    {
        _kills += change;
    }

}
