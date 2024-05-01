using Fusion;
using UnityEngine;

using System;
using UnityEngine.InputSystem;

public class PlayerMovementInput : NetworkBehaviour
{
    [SerializeField] private InputActionReference _actionReference;

    public override void Spawned()
    {
        Runner.GetComponent<NetworkEvents>().OnInput.AddListener(OnInputLeftStick);

    }

   
    public void OnInputLeftStick(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        Vector2 moveDirection = _actionReference.action.ReadValue<Vector2>();
        

        data.direction = moveDirection;
        input.Set(data);
    }
}
