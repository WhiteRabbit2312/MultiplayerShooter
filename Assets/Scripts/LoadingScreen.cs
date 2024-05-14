using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class LoadingScreen : NetworkBehaviour
{
    [SerializeField] private GameObject _waitingBoard;
    [SerializeField] private TextMeshProUGUI _loadingSession;

    private void Awake()
    {
        GameManager.OnStartGame += LoadingScreenPanel;
    }

    [Rpc(RpcSources.All, RpcTargets.All, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_SendMessagePanel()
    {
        RPC_RelayMessagePanel();
    }

    [Rpc(RpcSources.All, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_RelayMessagePanel()
    {
        _waitingBoard.SetActive(false);
    }

    private void LoadingScreenPanel()
    {
        StartCoroutine(DisableLoadScreen());

    }

    private IEnumerator DisableLoadScreen()
    {
        _loadingSession.text = "Loading session...";
        yield return new WaitForSeconds(5f);
        RPC_RelayMessagePanel();
    }
}
