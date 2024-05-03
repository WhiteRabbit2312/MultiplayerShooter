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

    private bool _mouseButton0;
    private void Update()
    {
        _mouseButton0 = _mouseButton0 | Input.GetMouseButton(0);
    }

    public void OnInputLeftStick(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        Vector2 moveDirection = _actionReference.action.ReadValue<Vector2>();

        data.direction = moveDirection;

        data.buttons.Set(NetworkInputData.MOUSEBUTTON0, _mouseButton0);
        _mouseButton0 = false;

        input.Set(data);
    }
}
