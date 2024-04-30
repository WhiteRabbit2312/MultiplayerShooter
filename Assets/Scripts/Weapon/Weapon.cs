using UnityEngine;

namespace SecondTraineeGame
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Sprite[] _weapon;
        [SerializeField] protected GameObject[] _bullet;

        public abstract int WeaponType();
        public abstract GameObject Bullet();
        public abstract void Damage(GameObject bullet);
        public abstract int FireRange();
    }
}
