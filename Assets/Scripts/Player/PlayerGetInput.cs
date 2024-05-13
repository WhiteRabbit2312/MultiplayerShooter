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

    private SpriteRenderer _playerSprite;

    private PlayerWeapon _playerWeapon;
    private PlayerHealth _playerHealth;
    private PlayerStats _playerStats;

    private int _coolDown = 0;
    private int _timer = 0;
    private int _damagePerTime = 60;
    private int _enemyDamage;


    [Networked] private bool _damaged { get; set; } = false;

    public override void Spawned()
    {
        _playerWeapon = GetComponentInChildren<PlayerWeapon>();
        _playerSprite = _player.GetComponent<SpriteRenderer>();
        _playerAnimator = GetComponentInChildren<Animator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerStats = GetComponent<PlayerStats>();

    }


    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data) && !_playerStats.Dead)
        {
            transform.Translate(data.directionMove * _speed);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x , -_maxX, _maxX), Mathf.Clamp(transform.position.y , -_maxY, _maxY), 0);

            if (_damaged)
            {
                if (_timer == _damagePerTime)
                {
                    _timer = 0;
                    _playerAnimator.SetBool("Damage", true);
                    _playerStats.GetDamage(_enemyDamage);
                }
                _timer++;
            }

            else
            {
                _playerAnimator.SetBool("Damage", false);
                if (data.directionMove.magnitude > 0)
                    _playerAnimator.SetBool("Go", true);


                else _playerAnimator.SetBool("Go", false);
            }

            


            if (data.directionShoot.magnitude > 0)
            {
                Vector2 direction = data.directionShoot;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
                if(_gun.transform.rotation.z >= 0.7f || _gun.transform.rotation.z <= -0.7f)
                {
                    _playerSprite.flipX = true;
                }

                else
                {
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
                        _playerStats.UseAmmo();

                    }
                    _coolDown = 0;

                }
            }
        }

        else if (_playerStats.Dead)
        {
            Debug.LogWarning("Dead animation");
            /*
            _playerAnimator.SetBool("Go", false);
            _playerAnimator.SetBool("Idle", false);
            _playerAnimator.SetBool("Damage", false);
            */
            _playerAnimator.SetTrigger("death");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy enemy))
        {
            _damaged = true;
            _enemyDamage = enemy.Damage;
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            _damaged = false;
    }
}
