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

        public override GameObject Bullet()
        {
            return _bullet[_bulletTypeIdx];
        }

        public override void Damage(GameObject bullet)
        {
            Instantiate(bullet);
        }

        public override int FireRange()
        {
            return _range;
        }
    }
}
