using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : NetworkBehaviour
{
    [SerializeField] private InputActionReference _actionReferenceLeftStick;
    [SerializeField] private InputActionReference _actionReferenceRightStick;

    public override void Spawned()
    {
        Runner.GetComponent<NetworkEvents>().OnInput.AddListener(OnInputCombine);
    }

    public void OnInputCombine(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();
        data.directionMove = OnInputPlayer(_actionReferenceLeftStick);
        data.directionShoot = OnInputPlayer(_actionReferenceRightStick);
        input.Set(data);
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
}
