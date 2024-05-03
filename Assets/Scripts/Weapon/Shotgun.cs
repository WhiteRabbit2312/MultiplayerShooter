using UnityEngine;

namespace SecondTraineeGame {
    public class Shotgun : Weapon
    {
        private int _weaponTypeIdx = 2;
        private int _bulletTypeIdx = 2;
        private int _damage = 2;
        private int _range = 3;
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
            for(int i = 0; i < 3; ++i)
            {
                if (Runner == null)
                    Debug.LogWarning("RnnerShotgun is null");
                else
                    Runner.Spawn(bullet, firePoint.position, Quaternion.identity);
            }
        }

        public override int FireRange()
        {
            return _range;
        }
    }
}
