using Fusion;
using UnityEngine;
using TMPro;

public class PlayerInput : NetworkBehaviour
{
    [SerializeField] private Sprite[] _skin;
    [Networked]
    public bool spawnedProjectile { get; set; }
    public override void Spawned()
    {
        Debug.Log("PlayerSpawned");
        Runner.GetComponent<NetworkEvents>().OnInput.AddListener(OnInputLeftStick);
        //int _skinIdx = PlayerPrefs.GetInt("Skin");
        //gameObject.GetComponent<SpriteRenderer>().sprite = _skin[_skinIdx];
    }

    public void OnInputLeftStick(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();
        
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("W input");
            data.direction += Vector3.up;
        }
            

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("S input");
            data.direction += Vector3.down;
        }
            

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A input");
            data.direction += Vector3.left;
        }
            

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D input");
            data.direction += Vector3.right;
        }
            

        input.Set(data);
    }

    public void OnInputRightStick()
    {

    }

    private void SpawnWeapon()
    {

    }
}
