using System.Collections;
using System.Collections.Generic;
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

    public override void Spawned()
    {
        if (!HasInputAuthority) return;
        OnPlayerStay += Stay;
        OnPlayerMove += Move;
        OnPlayerDamage += Damage;
        OnPlayerDeath += Death;

        Debug.Log("Animator set");

        _playerAnimator = GetComponent<Animator>();
        int skinIdx = PlayerPrefs.GetInt("Skin");
        _playerAnimator.runtimeAnimatorController = _animatorController[skinIdx];

    }

    public void Stay()
    {
        //_playerAnimator.Play("Idle");
        _playerAnimator.SetBool("Go", false);
    }
    public void Move()
    {
        _playerAnimator.SetBool("Go", true);
    }

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
