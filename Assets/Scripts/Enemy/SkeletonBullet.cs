using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SkeletonBullet : NetworkBehaviour
{
    private BasicSpawner _basicSpawner;
    public override void Spawned()
    {
        _basicSpawner = GameObject.FindObjectOfType<BasicSpawner>();
    }

    public override void FixedUpdateNetwork()
    {
        transform.Translate(_basicSpawner.CharacterPosition[0].position * Time.deltaTime);
    }
}
