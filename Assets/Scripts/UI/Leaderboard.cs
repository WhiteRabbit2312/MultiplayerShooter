using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class Leaderboard : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _kills;
    [SerializeField] private TextMeshProUGUI[] _damage;
    [SerializeField] private GameObject _leaderBoard;

    private BasicSpawner _basicspawner;

    public void Awake()
    {
        GameManager.OnGameOver += KillOnLeaderboard;
    }

    [Rpc(RpcSources.All, RpcTargets.All, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_SendMessage()
    {
        int[] kill = new int[2];
        int[] damage = new int[2];

        _basicspawner = FindObjectOfType<BasicSpawner>();
        int i = 0;
        foreach (var item in _basicspawner.SpawnedCharactersStats)
        {
            _kills[i].text = "Kills " + item.Value.Kills.ToString();
            kill[i] = item.Value.Kills;

            _damage[i].text = "Damage " + item.Value.Damage.ToString();
            damage[i] = item.Value.Damage;
            i++;
        }

        RPC_RelayMessage(kill, damage);
    }

    [Rpc(RpcSources.All, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_RelayMessage(int[] textKill, int[] textDamage)
    {
        _leaderBoard.SetActive(true);

        for(int i = 0; i < textKill.Length; ++i)
        {
            _kills[i].text = "Kills: " + textKill[i].ToString();
            _damage[i].text = "Damage: " + textDamage[i].ToString();
        }
    }

    private void KillOnLeaderboard()
    {
        RPC_SendMessage();
        
    }
}
