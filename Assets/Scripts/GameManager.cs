using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class GameManager : NetworkBehaviour
{
    public static event Action OnBreak;
    public static event Action OnGameplay;
    public static event Action OnDeath;

    public static void Break()
    {
        OnBreak?.Invoke();
    }

    public static void Gameplay()
    {
        OnGameplay?.Invoke();
    }

    public static void Death()
    {
        OnDeath?.Invoke();
    }
}
