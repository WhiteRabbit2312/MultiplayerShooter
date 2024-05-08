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
        OnPlayerStay += Stay;
        OnPlayerMove += Move;
        OnPlayerDamage += Damage;
        OnPlayerDeath += Death;


        _playerAnimator = GetComponent<Animator>();
        int skinIdx = PlayerPrefs.GetInt("Skin");
        _playerAnimator.runtimeAnimatorController = _animatorController[skinIdx];
    }

    public void Stay()
    {
        _playerAnimator.Play("Idle");
    }
    public void Move()
    {
        _playerAnimator.Play("Go");
    }

    public void Damage()
    {
        Debug.LogWarning("Player damage");
        _playerAnimator.Play("Damage");
    }

    public void Death()
    {
        Debug.LogWarning("Dead animation");
        _playerAnimator.Play("Death");
    }
}
