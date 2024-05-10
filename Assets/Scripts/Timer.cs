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
    [Networked] private int _breakTimer { get; set; }
    private bool _canRunTime = false;
    private bool _startGame = false;
    
    public override void Spawned()
    {
        GameManager.OnGameplay += SetTime;
        GameManager.OnStartGame += StartGame;
        GameManager.OnBreak += StopCount;
        int waveIdx = _waveManager.WaveCount;
        _breakTimer = _wave.waveStat[waveIdx].Break;
    }

    private void StartGame()
    {
        _startGame = true;
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
        if (_startGame)
        {
            if (_canRunTime)
            {
                TimeCount(_timer);
                _timer--;

                if (_timer == 0)
                {
                    int waveIdx = _waveManager.WaveCount;
                    _breakTimer = _wave.waveStat[waveIdx].Break;
                    _canRunTime = false;
                    GameManager.Break();
                    
                }
            }

            else
            {

                
                _breakTimer--;
                RPC_changeBreakTime();
                if (_breakTimer == 0)
                {
                    GameManager.Gameplay();
                }
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

    [Rpc]
    private void RPC_changeBreakTime()
    {
        _breakText.text = "Break time " + (_breakTimer / 50).ToString();
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        Debug.Log("Despawned");

        GameManager.OnGameplay -= SetTime;
        GameManager.OnBreak -= StopCount;
    }
}
