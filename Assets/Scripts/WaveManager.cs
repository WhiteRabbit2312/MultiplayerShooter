using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class WaveManager : NetworkBehaviour
{
    [HideInInspector] public int WaveCount = 0;

    public override void Spawned()
    {
        GameManager.OnBreak += NextWave;
    }

    public void NextWave() => WaveCount++;

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        Debug.Log("Despawned");

        GameManager.OnBreak -= NextWave;

    }
}
