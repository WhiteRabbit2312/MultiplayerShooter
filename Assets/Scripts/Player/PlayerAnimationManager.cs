using UnityEngine;
using Fusion;
using System;

public class PlayerAnimationManager : NetworkBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] _animatorController;
    public static Action OnPlayerStay;
    public static Action OnPlayerMove;
    public static Action OnPlayerDamage;
    public static Action OnPlayerDeath;

    private Animator _playerAnimator;
    private ChangeDetector _changeDetector;
    [Networked] private int _skinIdx { get; set; }

    public override void Spawned()
    {
        
        

        _playerAnimator = GetComponent<Animator>();
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
        _skinIdx = 0;
        RPC_ChangeSkinID(PlayerPrefs.GetInt("Skin"));
       
           
        OnPlayerDamage += Damage;
        OnPlayerDeath += Death;
       
    }
    private void ChangeSkin()
    {
        _playerAnimator.runtimeAnimatorController = _animatorController[_skinIdx];
    }

    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {
                case nameof(_skinIdx):
                    Debug.LogWarning("Index: " + _skinIdx);
                    
                    ChangeSkin();
                        break;
            }
        }
    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, InvokeLocal =  true)]
    private void RPC_ChangeSkinID(int id)
    {
        _skinIdx = id;
    }

    /*
    [Rpc(RpcSources.All, RpcTargets.All)]


    public void RPC_SendMessagReadySkin()
    {
        if (!Object.HasStateAuthority) return;
        _skinIdx = PlayerPrefs.GetInt("Skin");

        Debug.LogWarning("Skin idx " + _skinIdx);

        _playerAnimator.runtimeAnimatorController = _animatorController[_skinIdx];

    }*/
    /*
    public void Stay()
    {
        //_playerAnimator.Play("Idle");
        _playerAnimator.SetBool("Go", false);
    }
    public void Move()
    {
        _playerAnimator.SetBool("Go", true);
    }
    */
    public void Damage()
    {
        Debug.LogWarning("Player damage");
        _playerAnimator.SetTrigger("Damage");
    }

    public void Death()
    {
        Debug.LogWarning("Dead animation");
        _playerAnimator.SetTrigger("Death");
    }
}
