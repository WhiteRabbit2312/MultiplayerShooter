using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace SecondTraineeGame {
    public class PlayerWeapon : NetworkBehaviour
    {
        [SerializeField] private Sprite[] _weaponSprites;  
        private Weapon _weapon;
        private int _weaponCount = 3;
        public override void Spawned()
        {
            int weaponType = Random.Range(0, _weaponCount);

            switch (weaponType)
            {
                case 0:
                    _weapon = gameObject.AddComponent<Pistol>(); break;
                case 1: 
                    _weapon = gameObject.AddComponent<Shotgun>(); break;
                case 2:
                    _weapon = gameObject.AddComponent<MachineGun>(); break;
            }

            Debug.Log("Weapon type " + weaponType);

            gameObject.GetComponent<SpriteRenderer>().sprite = _weaponSprites[_weapon.WeaponType()];


        }


    }
}
