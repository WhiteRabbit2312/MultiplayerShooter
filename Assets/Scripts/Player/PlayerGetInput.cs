using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class PlayerGetInput : NetworkBehaviour //TODO: Player Movement
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _player;
    [SerializeField] private int _maxY;
    [SerializeField] private int _maxX;

    private Animator _playerAnimator;


    private SpriteRenderer _gunSprite;
    private SpriteRenderer _playerSprite;
    private PlayerWeapon _playerWeapon;
    private int _coolDown = 0;
    private bool _isFliped = false;
    private bool _isDead = false;
    private PlayerStats _playerStats;

    public override void Spawned()
    {
        _playerWeapon = GetComponentInChildren<PlayerWeapon>();
        _gunSprite = _gun.GetComponent<SpriteRenderer>();
        _playerSprite = _player.GetComponent<SpriteRenderer>();
        _playerStats = GetComponent<PlayerStats>();
        _playerAnimator = GetComponentInChildren<Animator>();
    }


    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            transform.Translate(data.directionMove * _speed);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x , -_maxX, _maxX), Mathf.Clamp(transform.position.y , -_maxY, _maxY), 0);
            if (data.directionMove.magnitude > 0)
                _playerAnimator.SetBool("Go", true);

            else _playerAnimator.SetBool("Go", false);

            /*
            if (_playerStats.Dead)
            {

                _playerAnimator.SetBool("Death", true);
                _gun.SetActive(false);
            }*/

            if (data.directionShoot.magnitude > 0)
            {
                Vector2 direction = data.directionShoot;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
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

                if (_coolDown == 10)
                {
                    if (_playerWeapon != null)
                    {
                        if (Runner.IsServer)
                        {
                            Bullet no = _playerWeapon.Shoot();
                            no.SetDirection(_gun.transform.rotation);
                            
                            Debug.LogWarning("Shooting");

                        }
                    }
                    _coolDown = 0;

                }
            }
        }
    }
}
