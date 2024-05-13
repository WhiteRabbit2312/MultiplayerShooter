using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class GameManager : NetworkBehaviour
{
    public static event Action OnStartGame;
    public static event Action OnBreak;
    public static event Action OnGameplay;
    public static event Action OnGameOver;

    private BasicSpawner _basicspawner;

    public override void Spawned()
    {
        _basicspawner = FindObjectOfType<BasicSpawner>();
    }

    public static void StartGame()
    {
        OnStartGame?.Invoke();
    }

    public static void Break()
    {
        OnBreak?.Invoke();
    }

    public static void Gameplay()
    {
        OnGameplay?.Invoke();
    }

    bool _once = true;// TODO

    public override void FixedUpdateNetwork()
    {
        if(_basicspawner.SpawnedCharactersStats.Count == 2 && _once)
        {
            StartGame();
            _once = false;
        }

        if (CheckPlayers())
        {
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
