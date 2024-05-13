using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;
using TMPro;

public class PlayerStats : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(HPChanged))] private int _hp { get; set; } = 15;
    [Networked, OnChangedRender(nameof(AmmoChanged))] private int _ammo { get; set; } = 80;
    [Networked, OnChangedRender(nameof(Test))] public int Kills { get; set; } = 0;
    [Networked] public int Damage { get; set; } = 0;

    [Networked] public bool Dead { get; set; } = false;
    public static Action OnKill;
    public static Action<int> OnDamage;
    public static Action<int> OnAmmo;

    private Animator _playerAnimator;
    private const int AdditionlHP = 4;
    private const int AdditionalAmmo = 80;

    private void Test()
    {
        if (HasInputAuthority)
            ShowPlayerStats.OnKillsChanged?.Invoke(Kills);
        Debug.LogError("ASDAFEKFSEKFN<SEMN");
    }

    private void HPChanged()
    {
        if (HasInputAuthority)
        {
            ShowPlayerStats.OnHPChanged?.Invoke(_hp);
        }
    }

    private void AmmoChanged()
    {
        if (HasInputAuthority)
        {
            ShowPlayerStats.OnAmmoChanged?.Invoke(_ammo);
        }
    }

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
        _hp += AdditionlHP;

        if (_hp > 15)
            _hp = 15;
        if (HasInputAuthority)
        {
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
        Kills++;
        Debug.LogWarning("Kill");
        if (HasInputAuthority)
        {
            Debug.LogWarning("Has input authority kill");
            ShowPlayerStats.OnKillsChanged?.Invoke(Kills);

        }
    }

    public void ChangeDamage(int damage)
    {
        
        Damage += damage;

    }

}
