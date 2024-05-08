using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class AmmoBox : NetworkBehaviour, IItemEffect
{
    public void EnableEffect(PlayerStats playerStats)
    {
        playerStats.TakeAmmoBox();
        Runner.Despawn(Object);
    }
}
