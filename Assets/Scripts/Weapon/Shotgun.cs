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

        public override GameObject Bullet()
        {
            return _bullet[_bulletTypeIdx];
        }

        public override void Damage(GameObject bullet)
        {
            for(int i = 0; i < 3; ++i)
            {
                Instantiate(bullet);
            }
        }

        public override int FireRange()
        {
            return _range;
        }
    }
}
