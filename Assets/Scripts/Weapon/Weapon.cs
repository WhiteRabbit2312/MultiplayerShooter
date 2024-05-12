using UnityEngine;
using Fusion;


    public abstract class Weapon : NetworkBehaviour
    {
        [SerializeField] protected Sprite[] _weapon;
        [SerializeField] protected GameObject[] _bullet;

        public abstract int WeaponType();
        public abstract int BulletType();
        public abstract void Fire();
        public abstract bool HasBullets();
        public abstract void TakeBullet();

        public abstract int Bullets
        {
            get; set;
        }
    }

