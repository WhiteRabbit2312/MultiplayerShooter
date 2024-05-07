using Fusion;
using UnityEngine;

using System;
using UnityEngine.InputSystem;
using SecondTraineeGame;

public class PlayerMovementInput : NetworkBehaviour
{
    [SerializeField] private InputActionReference _actionReferenceLeftStick;
    [SerializeField] private InputActionReference _actionReferenceRightStick;

    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _player;

    private SpriteRenderer _gunSprite;
    private SpriteRenderer _playerSprite;
    private PlayerWeapon _playerWeapon;

    public override void Spawned()
    {
        Runner.GetComponent<NetworkEvents>().OnInput.AddListener(OnInputCombine);

        _gunSprite = _gun.GetComponent<SpriteRenderer>();
        _playerWeapon = GetComponentInChildren<PlayerWeapon>();

        _playerSprite = _player.GetComponent<SpriteRenderer>();
    }

    public void OnInputCombine(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

//#if UNITY_EDITOR
//        data.directionMove += OnInputTest();
//#elif UNITY_ANDROID

        data.directionMove = OnInputPlayer(_actionReferenceLeftStick);
        data.directionShoot = OnInputPlayer(_actionReferenceRightStick);
        input.Set(data);


        //#endif
        //data.directionShoot = OnInputPlayer(_actionReferenceRightStick);


    }

    public void OnInputShoot(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();
        data.directionShoot = OnInputPlayer(_actionReferenceRightStick);
        input.Set(data);
    }


    public Vector2 OnInputPlayer(InputActionReference actionReference)
    {
        Vector2 moveDirection = actionReference.action.ReadValue<Vector2>();
        return moveDirection;
    }
    /*
    public Vector3 OnInputTest()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("W input");
            return Vector3.up;
        }


        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("S input");
            return Vector3.down;
        }


        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A input");
            return Vector3.left;
        }


        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D input");
            return Vector3.right;
        }

        return default;
    }*/
}
