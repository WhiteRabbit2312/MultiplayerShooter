using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;
using TMPro;

public class PlayerStats : NetworkBehaviour
{
    [Networked] private int _hp { get; set; } = 15;
    private int _ammo { get; set; } = 80;
    [Networked] public int Kills { get; set; } = 0;
    [Networked] public int Damage { get; set; } = 0;

    [Networked] public bool Dead { get; set; } = false;
    public static Action OnKill;
    public static Action<int> OnDamage;
    public static Action<int> OnAmmo;

    private Animator _playerAnimator;
    private const int AdditionlHP = 4;
    private const int AdditionalAmmo = 80;


    public override void Spawned()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        OnKill += ChangeKills;
        OnDamage += ChangeDamage;
    }

    public void GetDamage(int change)
    {

        if (_hp > 0)
        {
            _hp -= change;
        }

        else
        {
            _hp = 0;
            Dead = true;
        }


        if (HasInputAuthority)
        {
            ShowPlayerStats.OnHPChanged?.Invoke(_hp);
        }
    }

    public void TakeAidKit()
    {
        if (HasInputAuthority)
        {

            _hp += AdditionlHP;

            if (_hp > 15)
                _hp = 15;
            ShowPlayerStats.OnHPChanged?.Invoke(_hp);
        }
    }


    public void UseAmmo()
    {
        if(_ammo > 0)
            _ammo--;

        if (HasInputAuthority)
        {
            ShowPlayerStats.OnAmmoChanged?.Invoke(_ammo);
        }

    }

    public void TakeAmmoBox()
    {
        _ammo = AdditionalAmmo;


        if (HasInputAuthority)
        {
            ShowPlayerStats.OnAmmoChanged?.Invoke(_ammo);
        }
    }

    public void ChangeKills()
    {
        
        if (HasInputAuthority)
        {
            Kills++;
            ShowPlayerStats.OnKillsChanged?.Invoke(Kills);
        }
    }

    public void ChangeDamage(int damage)
    {
        
        Damage += damage;

    }

}
