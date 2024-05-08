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
    public static event Action OnGameOver;

    private BasicSpawner _basicspawner;

    public override void Spawned()
    {
        _basicspawner = FindObjectOfType<BasicSpawner>();
    }

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

    public override void FixedUpdateNetwork()
    {
        

        if (CheckPlayers())
        {
            Debug.LogWarning("Invoked");
            OnGameOver?.Invoke();
        }
    }

    public bool CheckPlayers()
    {

        
        foreach (var item in _basicspawner.SpawnedCharactersStats)
        {
            if (!item.Value.Dead)
                return false;
        }

        return true;
    }
}
