using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WaveStat
{
    public int Enemy;
    public int Item;
    public int Timer;
    public int Break;
}

[CreateAssetMenu(fileName = "Wave", menuName = "Data")]
public class Wave : ScriptableObject
{
    public List<WaveStat> waveStat;
}
