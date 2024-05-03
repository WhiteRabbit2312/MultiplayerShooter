using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fusion;
using TMPro;

public class PlayerStatChanged : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private TextMeshProUGUI _killText;

    public static Action<int> OnHPChanged;
    public static Action<int> OnAmmoChanged;
    public static Action<int> OnFragChanged;

    public override void Spawned()
    {
        OnHPChanged += ChangeHP;
        OnAmmoChanged += ChangeAmmo;
        OnFragChanged += ChangeKills;
    }
    private const int FullHP = 15;
    private const int FullAmmo = 10;

    private int _hp = 15;
    private int _ammo = 10;
    private int _kills = 0;

    private void ChangeHP(int change)
    {
        if(_hp <= FullHP)
            _hp += change;

        if (_hp > FullHP)
            _hp = FullHP;

        _hpText.text = "HP " + _hp.ToString() + "/15";
        Debug.LogWarning("HP " + _hp);
    }

    private void ChangeAmmo(int change)
    {
        if (_hp <= FullHP)
            _ammo += change;

        if (_ammo > FullAmmo)
            _ammo = FullAmmo;

        _ammo += change;
        _ammoText.text = "Ammo " + _ammo.ToString() + "/10";
        Debug.LogWarning("Ammo " + _ammo);
    }

    private void ChangeKills(int change)
    {
        _kills += change;
        _killText.text = "Kills " + _kills.ToString();
        Debug.LogWarning("Kill " + _kills);
    }

}
