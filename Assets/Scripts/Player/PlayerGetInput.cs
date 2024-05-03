using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using SecondTraineeGame;

public class PlayerGetInput : NetworkBehaviour //TODO: Player Movement
{
    [SerializeField] private float _speed = 5f;
    private Animator _playerAnimator;
    private PlayerWeapon _playerWeapon;
    //transform.Translate(moveDirection * _speed * Time.deltaTime);

    public override void Spawned()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        _playerWeapon = GetComponent<PlayerWeapon>();
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            transform.Translate(data.direction * _speed);

            if (data.direction.magnitude > 0)
                _playerAnimator.Play("Go");

            else _playerAnimator.Play("Idle");

            if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
            {
                _playerWeapon.Shoot();
            }
        }
    }
}
