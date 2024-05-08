using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace SecondTraineeGame
{
    public class PlayerWeapon : NetworkBehaviour
    {
        [SerializeField] private GameObject[] _bullet;
        [SerializeField] private Transform _weaponPoint;
        [SerializeField] private Sprite[] _weaponSprites;
        private Weapon _weapon;
        private int _weaponCount = 3;
        private SpriteRenderer _playerSprite;
        [Networked] private int weaponType { get; set; }

        private void Awake()
        {
            _playerSprite = gameObject.GetComponent<SpriteRenderer>();
        }
        public override void Spawned()
        {
            //_weapon = gameObject.AddComponent<Pistol>();
            //if (!Object.HasStateAuthority)
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
            SetupSkinForEveryone();

        }

        private NetworkObject _spawnedBulletNetworkObject;
        private Bullet _spawnedBullet;
        public Bullet Shoot()
        {
     

            if (_weapon != null)
            {
                _spawnedBulletNetworkObject = Runner.Spawn(_bullet[_weapon.BulletType()], _weaponPoint.position, Quaternion.identity);
                _spawnedBullet = _spawnedBulletNetworkObject.GetComponent<Bullet>();
                PlayerStats.OnDamage.Invoke(_spawnedBullet.Damage);
            }

            else Debug.LogError("Weapon is null");

            return _spawnedBullet;
        }

        private void SetupSkinForEveryone()
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
}
