using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public enum WaveState
{
    First,
    Second,
    Third
}

public class WaveManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private int _timer;
    public WaveState State;


    
}
