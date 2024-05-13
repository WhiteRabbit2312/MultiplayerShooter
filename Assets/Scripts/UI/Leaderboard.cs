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
        Debug.LogError("Send Message");
        RPC_RelayMessage();
    }

    [Rpc(RpcSources.All, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_RelayMessage()
    {
        Debug.LogError("RPC_RelayMessage");
        _leaderBoard.SetActive(true);
    }

  

    private void KillOnLeaderboard()
    {
        Debug.LogError("Kill on leaderboard");
        RPC_SendMessage();
        _basicspawner = FindObjectOfType<BasicSpawner>();
        int i = 0;
        foreach(var item in _basicspawner.SpawnedCharactersStats)
        {
            _kills[i].text = "Kills " + item.Value.Kills.ToString();
            _damage[i].text = "Damage " + item.Value.Damage.ToString();
            i++;
        }
    }
}
