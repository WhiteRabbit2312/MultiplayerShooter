using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class AmmoBox : NetworkBehaviour, IItemEffect
{
    //[SerializeField] private TextMeshProUGUI _ammoText;
    private int _fullAmmo = 10;
    private int _ammo;

    public void EnableEffect()
    {
        _ammo = _fullAmmo;
        //_ammoText.text = "Ammo " + _ammo.ToString() + "/10";

        Debug.LogWarning("AmmoBox" + _ammo);
        Runner.Despawn(Object);
    }
}
