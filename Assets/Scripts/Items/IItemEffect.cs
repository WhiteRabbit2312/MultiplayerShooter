using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemEffect 
{
    public void EnableEffect(PlayerWeapon playerWeapon, PlayerStats playerStats);
}
