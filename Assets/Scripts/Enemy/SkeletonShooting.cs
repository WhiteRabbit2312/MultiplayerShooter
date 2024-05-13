using UnityEngine;
using Fusion;

public class SkeletonShooting : NetworkBehaviour
{
    [SerializeField] private GameObject _bullet;
    private GameObject _player;
    private int _spawnPerSecond = 150;
    private int _timer = 0;

    public override void Spawned()
    {
        int randomPlayer = Random.Range(0, 2);
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        _player = player[randomPlayer];
    }

    public override void FixedUpdateNetwork()
    {
        Debug.LogWarning("Spawn skeleton bullet");
        if(_timer == _spawnPerSecond)
        {
            NetworkObject netwObj = Runner.Spawn(_bullet, transform.position);
            SkeletonBullet skeletonBullet = netwObj.GetComponent<SkeletonBullet>();
            skeletonBullet.Init(_player.transform);

            _timer = 0;
        }
        _timer++;
    }
}
