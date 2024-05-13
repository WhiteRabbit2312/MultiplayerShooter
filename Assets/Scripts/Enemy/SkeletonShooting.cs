using UnityEngine;
using Fusion;

public class SkeletonShooting : NetworkBehaviour
{
    [SerializeField] private GameObject _bullet;
    private int _spawnPerSecond = 50;
    private int _timer = 0;

    public override void FixedUpdateNetwork()
    {
        Debug.LogWarning("Spawn skeleton bullet");
        if(_timer == _spawnPerSecond)
        {
            Runner.Spawn(_bullet, transform.position);
            _timer = 0;
        }
        _timer++;
    }
}
