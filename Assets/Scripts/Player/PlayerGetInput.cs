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
                    
                    RPC_SendMessageFlipRight();
                }

                else
                {
                    RPC_SendMessageFlipLeft();
                   
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
                        }
                        _playerStats.UseAmmo();

                    }
                    _coolDown = 0;

                }
            }
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_SendMessageFlipRight()
    {
        RPC_GunFlipRight();
    }

    [Rpc(RpcSources.All, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_GunFlipRight()
    {
        _playerSprite.flipX = true;
        _gun.GetComponent<SpriteRenderer>().flipY = true;
    }

    [Rpc(RpcSources.All, RpcTargets.All, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_SendMessageFlipLeft()
    {
        RPC_GunFlipLeft();
    }

    [Rpc(RpcSources.All, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_GunFlipLeft()
    {
        _gun.GetComponent<SpriteRenderer>().flipY = false;
        _playerSprite.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy enemy))
        {
            _damaged = true;
            _enemyDamage = enemy.Damage;
        }
            
        if(collision.gameObject.tag == "SkeletonEnemy")
        {
            _playerAnimator.SetBool("Damage", true);
            _playerStats.GetDamage(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            _damaged = false;
    }
}
