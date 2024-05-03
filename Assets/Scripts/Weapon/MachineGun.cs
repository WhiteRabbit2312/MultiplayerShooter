using UnityEngine;
namespace SecondTraineeGame
{
    public class MachineGun : Weapon
    {
        private int _weaponTypeIdx = 0;
        private int _bulletTypeIdx = 0;
        private int _damage = 2;
        private int _range = 3;
        public override int WeaponType()
        {
            return _weaponTypeIdx;
        }

        public override int BulletType()
        {
            return _bulletTypeIdx;
        }

        public override void Fire(GameObject bullet, Transform firePoint)
        {
            Runner.Spawn(bullet, firePoint.position, Quaternion.identity);
        }

        public override int FireRange()
        {
            return _range;
        }
    }
}
