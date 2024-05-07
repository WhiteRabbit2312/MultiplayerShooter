using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using SecondTraineeGame;

public class PlayerGetInput : NetworkBehaviour //TODO: Player Movement
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _player;


    private SpriteRenderer _gunSprite;
    private SpriteRenderer _playerSprite;
    private PlayerWeapon _playerWeapon;
    private int _coolDown = 0;
    private bool _isFliped = false;

    public override void Spawned()
    {
        _playerWeapon = GetComponentInChildren<PlayerWeapon>();
        _gunSprite = _gun.GetComponent<SpriteRenderer>();
        _playerSprite = _player.GetComponent<SpriteRenderer>();
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            transform.Translate(data.directionMove * _speed);
            Debug.LogError($"Move {data.directionMove} | Shoot {data.directionShoot} ");

            //if (data.directionMove.magnitude > 0)
                //PlayerAnimationManager.OnPlayerMove?.Invoke(); //_playerAnimator.Play("Go");

            //else PlayerAnimationManager.OnPlayerStay?.Invoke(); //_playerAnimator.Play("Idle");
            /*
            
            if (data.directionShoot.magnitude > 0)
            {
                Vector2 direction = data.directionShoot;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                Debug.Log("Gun rotation " + _gun.transform.rotation);

                
                if(_gun.transform.rotation.z >= 0.7f || _gun.transform.rotation.z <= -0.7f)
                {
                    //_gunSprite.flipX = true;
                    _playerSprite.flipX = true;
                }

                else
                {
                    //_gunSprite.flipX = false;
                    _playerSprite.flipX = false;
                }
                
                _coolDown++;

                if (_coolDown == 100)
                {
                    Bullet no = _playerWeapon.Shoot();
                    no.SetDirection(_gun.transform.rotation);
                    _coolDown = 0;
                }
                
            }*/
        }
    }
}
