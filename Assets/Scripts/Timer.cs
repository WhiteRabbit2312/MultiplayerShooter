using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class Timer : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _breakText;
    [SerializeField] private Wave _wave;
    [SerializeField] private WaveManager _waveManager;

    [Networked] private int _minutes { get; set; }
    [Networked] private int _seconds { get; set; }

    private int _timer;
    private int _breakTimer;
    private bool _canRunTime = false;
    
    public override void Spawned()
    {
        GameManager.OnGameplay += SetTime;
        GameManager.OnBreak += StopCount;
        int waveIdx = _waveManager.WaveCount;
        _breakTimer = _wave.waveStat[waveIdx].Break;
    }

    private void SetTime() 
    {
        _canRunTime = true;
        int waveIdx = _waveManager.WaveCount;
        _timer = _wave.waveStat[waveIdx].Timer; 
        
    }

    private void StopCount()
    {
        _canRunTime = false;
    }

    public override void FixedUpdateNetwork()
    {
        if (_canRunTime)
        {
            TimeCount(_timer);
            _timer--;

            if (_timer == 0)
            {
                Debug.Log("Break");

                GameManager.Break();
                int waveIdx = _waveManager.WaveCount;
                _breakTimer = _wave.waveStat[waveIdx].Break;
            }
        }

        else
        {
            
            _breakText.text = "Break time " + (_breakTimer / 50).ToString();
            _breakTimer--;
            if(_breakTimer == 0)
            {
                Debug.Log("Gameplay");
                GameManager.Gameplay();
            }
        }
    }

    private void TimeCount(int timer)
    {
        _minutes = (timer / 50) / 60;
        _seconds = (timer / 50) % 60;
        //_timerText.text = _minutes + " : " + _seconds;
        RPC_changeTime();
    }

    [Rpc]
    private void RPC_changeTime()
    {
        _timerText.text = _minutes + " : " + _seconds;
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        Debug.Log("Despawned");

        GameManager.OnGameplay -= SetTime;
        GameManager.OnBreak -= StopCount;
    }
}
