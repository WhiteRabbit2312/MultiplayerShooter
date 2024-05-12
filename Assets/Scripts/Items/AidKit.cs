using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class AidKit : NetworkBehaviour, IItemEffect
{
    public void EnableEffect(PlayerWeapon playerWeapon, PlayerStats playerStats)
    {
        //playerStats.TakeAidKit();
        Runner.Despawn(Object);
    }
}
