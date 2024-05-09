using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _kills;
    [SerializeField] private TextMeshProUGUI[] _damage;
    [SerializeField] private GameObject _leaderBoard;

    private BasicSpawner _basicspawner;

    public void Awake()
    {
        _basicspawner = FindObjectOfType<BasicSpawner>();

        GameManager.OnGameOver += EnableaderBoard;
        GameManager.OnGameOver += KillOnLeaderboard;
    }

    private void EnableaderBoard()
    {
        _leaderBoard.SetActive(true);
    }

    private void KillOnLeaderboard()
    {
        int i = 0;
        foreach(var item in _basicspawner.SpawnedCharactersStats)
        {
            _kills[i].text = "Kills " + item.Value.Kills.ToString();
            _damage[i].text = "Damage " + item.Value.Damage.ToString();
            i++;
        }
    }
}
