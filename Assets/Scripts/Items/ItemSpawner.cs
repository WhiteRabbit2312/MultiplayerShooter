using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ItemSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject[] _item;
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private Wave _wave;
    [SerializeField] private WaveManager _waveManager;

    private int _timer = 0;
    private bool _canSpawn = false;
    

    public override void Spawned()
    {
        GameManager.OnGameplay += CanPlay;
        GameManager.OnBreak += StopPlay;
    }

    public void CanPlay() => _canSpawn = true;
    public void StopPlay() => _canSpawn = false;

    public override void FixedUpdateNetwork()
    {
        if (_canSpawn)
        {
            _timer++;
            if (_timer == 200)
            {
                int waveIdx = _waveManager.WaveCount;
                int randomItem = Random.Range(0, _wave.waveStat[waveIdx].Item);



                for(int i = 0; i < _spawnPoint.Length; ++i)
                {
                    if (!_spawnPoint[i].GetComponent<SpawnPointState>().State)
                    {
                        _spawnPoint[i].GetComponent<SpawnPointState>().State = true;
                        Runner.Spawn(_item[randomItem], _spawnPoint[i].position, Quaternion.identity);
                        Debug.LogError("Item type" + randomItem);
                        _timer = 0;
                        break;
                    }
                }

                
            }
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        Debug.Log("Despawned");

        GameManager.OnGameplay -= CanPlay;
        GameManager.OnBreak -= StopPlay;
    }
}
