using UnityEngine;
using Fusion;

namespace SecondTraineeGame
{
    public abstract class Weapon : NetworkBehaviour
    {
        [SerializeField] protected Sprite[] _weapon;
        [SerializeField] protected GameObject[] _bullet;

        public abstract int WeaponType();
        public abstract int BulletType();
        public abstract void Fire(GameObject bullet, Transform firePoint);
        public abstract int FireRange();
    }
}
