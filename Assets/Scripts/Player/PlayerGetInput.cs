using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using SecondTraineeGame;

public class PlayerGetInput : NetworkBehaviour //TODO: Player Movement
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private GameObject _gun;
    private Animator _playerAnimator;
    private PlayerWeapon _playerWeapon;
    //transform.Translate(moveDirection * _speed * Time.deltaTime);

    public override void Spawned()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        _playerWeapon = GetComponentInChildren<PlayerWeapon>();
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            transform.Translate(data.directionMove * _speed);

            if (data.directionMove.magnitude > 0)
                _playerAnimator.Play("Go");

            else _playerAnimator.Play("Idle");

            if(data.directionShoot.magnitude > 0)
            {
                Vector2 direction = data.directionShoot - _gun.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _gun.transform.rotation = Quaternion.AngleAxis(angle, Vector2.up);
                _playerWeapon.Shoot();
            }
        }
    }
}
