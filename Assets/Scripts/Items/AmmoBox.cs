using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class AmmoBox : NetworkBehaviour, IItemEffect
{
    public void EnableEffect(PlayerWeapon playerWeapon, PlayerStats playerStats)
    {
        playerStats.TakeAmmoBox();
        playerWeapon.TakeAmooBox();
        Runner.Despawn(Object);
    }
}
