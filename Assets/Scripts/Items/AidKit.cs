using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class AidKit : NetworkBehaviour, IItemEffect
{
    private int _hp;
    private int _fullHP = 15;
    public void EnableEffect()
    {
        _hp = _fullHP;
        Debug.LogWarning("HP" + _hp);
        Runner.Despawn(Object);
    }
}
