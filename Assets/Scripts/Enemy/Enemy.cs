using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Enemy : NetworkBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    private Vector2 _direction = Vector2.zero;
    private float _radius = 5f;

    private Transform _objectTransform;
    //private NetworkEnemyData _networkEnemyData;

    public override void Spawned()
    {
        if (!HasStateAuthority) return;
        //Runner.GetComponent<NetworkEvents>().OnInput.AddListener(OnEnemyMove);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_objectTransform.position, _radius);
    }

    public override void FixedUpdateNetwork()
    {
        Debug.Log("Enemy transform" + transform.position);

        if (!HasStateAuthority)
        {
            Debug.Log("HasStateAuthority");
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);


        foreach (Collider collider in colliders)
        {
            _objectTransform = collider.GetComponent<Transform>();
            
        }

        if (_objectTransform != null)
        {
            transform.position += _objectTransform.position * _speed;//not initialize
        }
        else
        {
            transform.position += new Vector3(0, 0, 0) * _speed;
        }

    }
    
    /*
    public void OnEnemyMove(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkEnemyData();
        data.direction = transform.position;
        input.Set(data);
    }*/
}
