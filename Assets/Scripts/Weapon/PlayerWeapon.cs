using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] private GameObject[] _bullet;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Sprite[] _weaponSprites;

    private Weapon _weapon;
    private int _weaponCount = 3;
    private SpriteRenderer _playerSprite;
    private Bullet _spawnedBullet;
    [Networked] private int weaponType { get; set; }

    private void Awake()
    {
        _playerSprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public override void Spawned()
    {
        RPC_SendMessagReady();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendMessagReady()
    {
        if (!Object.HasStateAuthority)
            return;

        weaponType = Random.Range(0, _weaponCount);

        switch (weaponType)
        {
            case 0:
                _weapon = gameObject.AddComponent<Pistol>(); break;
            case 1:
                _weapon = gameObject.AddComponent<Shotgun>(); break;
            case 2:
                _weapon = gameObject.AddComponent<MachineGun>(); break;
        }

        _playerSprite.sprite = _weaponSprites[_weapon.WeaponType()];
        SetupWeaponForEveryone();
    }

    
    public Bullet Shoot()
    {
        if (_weapon != null)
        {
            if (Runner != null && Runner.IsServer && _weapon.HasBullets())
            {
                NetworkObject _spawnedBulletNetworkObject = Runner.Spawn(_bullet[_weapon.BulletType()], _weaponPoint.position, Quaternion.identity);
                _spawnedBullet = _spawnedBulletNetworkObject.GetComponent<Bullet>();
                _weapon.Fire();
                ShowPlayerStats.OnAmmoChanged?.Invoke(_weapon.Bullets);
            }
        }

        else Debug.LogError("Weapon is null");

        return _spawnedBullet;
    }

    public void TakeAmooBox()
    {
        _weapon.TakeBullet();
        ShowPlayerStats.OnAmmoChanged?.Invoke(_weapon.Bullets);
    }

    private void SetupWeaponForEveryone()
    {
        if (_weapon != null)
        {
            Debug.Log("SetupSkinForEveryone");
            RPC_SendMessageWeapon(_weapon.WeaponType());
        }

    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendMessageWeapon(int weaponType)
    {
        Debug.Log("RPC_SendMessageWeapon");
        switch (weaponType)
        {
            case 0:
                _weapon = gameObject.AddComponent<Pistol>(); break;
            case 1:
                _weapon = gameObject.AddComponent<Shotgun>(); break;
            case 2:
                _weapon = gameObject.AddComponent<MachineGun>(); break;
        }
        _playerSprite.sprite = _weaponSprites[weaponType];
    }
}

