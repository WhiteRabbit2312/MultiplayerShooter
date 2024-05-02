using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace SecondTraineeGame
{
    public class PlayerWeapon : NetworkBehaviour
    {
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
            if (!Object.HasStateAuthority)
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
            _playerSprite.sprite = _weaponSprites[weaponType];
        }


    }
}
