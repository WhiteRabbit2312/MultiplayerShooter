using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class PlayerStats : NetworkBehaviour
{
    private int _hp = 15;
    private int _ammo = 1000;
    [HideInInspector] public int Kills = 0;
    [HideInInspector] public int Damage = 0;
    [HideInInspector] public bool Dead = false;
    public static Action OnKill;
    public static Action<int> OnDamage;

    private const int FullHP = 15;
    private const int FullAmmo = 1000;

    public override void Spawned()
    {
        OnKill += ChangeKills;
        OnDamage += ChangeDamage;
    }

    public void GetDamage(int change)
    {
        if (_hp > 0)
        {
            _hp -= change;
            
            PlayerAnimationManager.OnPlayerDamage?.Invoke();
        }

        else
        {
            _hp = 0;
            Dead = true;
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
        Debug.LogWarning("Client damaged");
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

    public void ChangeKills()
    {
        Kills++;
        ShowPlayerStats.OnKillsChanged?.Invoke(Kills);
    }

    public void ChangeDamage(int damage)
    {
        Damage += damage;
        
    }

}
