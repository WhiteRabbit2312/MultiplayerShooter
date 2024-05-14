using UnityEngine;


public class Pistol : Weapon
{
    private int _weaponTypeIdx = 1;
    private int _bulletTypeIdx = 1;
    private int _bulletAmount = 80;

    public override int Bullets
    {
        get; set;
    }

    public override int WeaponType()
    {
        return _weaponTypeIdx;
    }

    public override int BulletType()
    {
        return _bulletTypeIdx;
    }

    public override bool HasBullets()
    {
        if (_bulletAmount == 0)
            return false;
        else return true;
    }

    public override void Fire()
    {
        _bulletAmount--;
        Bullets = _bulletAmount;

    }

    public override void TakeBullet()
    {
        _bulletAmount = 80;
    }
}

