using UnityEngine;

namespace SecondTraineeGame
{
    public class Pistol : Weapon
    {
        private int _weaponTypeIdx = 1;
        private int _bulletTypeIdx = 1;
        private int _damage = 3;
        private int _range = 1;
        public override int WeaponType()
        {
            //Debug.Log("Shotgun");
            return _weaponTypeIdx;
        }

        public override int BulletType()
        {
            return _bulletTypeIdx;
        }

        public override void Fire(GameObject bullet, Transform firePoint)
        {
            if (Runner == null)
                Debug.LogWarning("Runner pistol is null");
            else
                Runner.Spawn(bullet, firePoint.position, Quaternion.identity);
        }

        public override int FireRange()
        {
            return _range;
        }
    }
}
