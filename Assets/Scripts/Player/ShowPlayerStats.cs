using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fusion;
using TMPro;

public class ShowPlayerStats : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private TextMeshProUGUI _killText;

    public static Action<int> OnHPChanged;
    public static Action<int> OnAmmoChanged;
    public static Action<int> OnKillsChanged;


    public override void Spawned()
    {
        OnHPChanged += HP;
        OnAmmoChanged += Ammo;
        OnKillsChanged += Kills;
    }

    private void HP(int hp)
    {
        _hpText.text = "HP " + hp.ToString() + "/15";
        Debug.LogWarning("Client");
    }

    private void Ammo(int ammo)
    {
        _ammoText.text = "Ammo: " + ammo.ToString() + "/1000";
    }

    private void Kills(int kill)
    {
        _killText.text = "Kills " + kill.ToString();
    }

}
