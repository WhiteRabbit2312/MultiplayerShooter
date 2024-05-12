using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;
using TMPro;

public class PlayerStats : NetworkBehaviour
{
    private int _hp { get; set; } = 15;
    private int _ammo { get; set; } = 80;
    public int Kills { get; set; } = 0;
    public int Damage { get; set; } = 0;

    [HideInInspector] public bool Dead = false;
    public static Action OnKill;
    public static Action<int> OnDamage;

    private Animator _playerAnimator;
    private const int AdditionlHP = 4;
    private const int AdditionalAmmo = 30;


    public override void Spawned()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        OnKill += ChangeKills;
        OnDamage += ChangeDamage;
    }

    public void GetDamage(int change)
    {
        

        if (HasInputAuthority)
        {
            if (_hp > 0)
            {
                _hp -= change;
                _playerAnimator.SetBool("Damage", true);
            }

            else
            {
                _hp = 0;
                Dead = true;
            }



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
        if (HasInputAuthority)
        {
            _ammo += AdditionalAmmo;
            if (_ammo > 80)
                _ammo = 80;
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
