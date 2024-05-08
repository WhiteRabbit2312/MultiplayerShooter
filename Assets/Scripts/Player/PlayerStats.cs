using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerStats : NetworkBehaviour
{
    private int _hp = 15;
    private int _ammo = 1000;
    private int _kills = 0;

    private const int FullHP = 15;
    private const int FullAmmo = 1000;

    public void GetDamage(int change)
    {
        if (_hp > 0)
        {
            _hp -= change;
            Debug.LogWarning("HP " + _hp);
            PlayerAnimationManager.OnPlayerDamage?.Invoke();
        }

        else
        {
            _hp = 0;
            GameManager.Death();
        }

        ShowPlayerStats.OnHPChanged?.Invoke(_hp);
    }

    public void TakeAidKit()
    {
        _hp = FullHP;
        ShowPlayerStats.OnHPChanged?.Invoke(_hp);
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

        ShowPlayerStats.OnAmmoChanged?.Invoke(_ammo);
    }

    public bool HaveAmmo()
    {
        return _ammo != 0 ? true : false;
    }

    public void TakeAmmoBox()
    {
        _ammo = FullAmmo;
        ShowPlayerStats.OnAmmoChanged?.Invoke(_ammo);
    }

    public void ChangeKills(int change)
    {
        _kills += change;
        ShowPlayerStats.OnKillsChanged?.Invoke(_kills);
    }

}
