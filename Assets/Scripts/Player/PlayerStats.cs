using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;
using TMPro;

public class PlayerStats : NetworkBehaviour
{
    private int _hp { get; set; } = 15;
    private int _ammo { get; set; } = 1000;
    public int Kills { get; set; } = 0;
    public int Damage { get; set; } = 0;

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
            PlayerAnimationManager.OnPlayerDeath?.Invoke();
        }

        if(HasInputAuthority)
        {
            ShowPlayerStats.OnHPChanged?.Invoke(_hp);
            
        }
    }

    public void TakeAidKit()
    {
        _hp = FullHP;

        if (HasInputAuthority)
        {
            ShowPlayerStats.OnHPChanged?.Invoke(_hp);
        }
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



        if (HasInputAuthority)
        {
            ShowPlayerStats.OnAmmoChanged?.Invoke(_ammo);
        }

    }

    public bool HaveAmmo()
    {
        return _ammo != 0 ? true : false;
    }

    public void TakeAmmoBox()
    {
        _ammo = FullAmmo;
        if (HasInputAuthority)
        {
            ShowPlayerStats.OnAmmoChanged?.Invoke(_ammo);
        }
    }

    public void ChangeKills()
    {
        Kills++;
        if (HasInputAuthority)
        {
            ShowPlayerStats.OnKillsChanged?.Invoke(Kills);
        }
    }

    public void ChangeDamage(int damage)
    {
        
        Damage += damage;

    }

}
