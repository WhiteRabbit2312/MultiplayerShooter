using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;
using SecondTraineeGame;

public class Shoot : NetworkBehaviour
{
    [SerializeField] private InputActionReference _actionReferenceRightStick;
    [SerializeField] private Transform _gun;
    [SerializeField] private GameObject _player;

    private SpriteRenderer _gunSprite;
    private SpriteRenderer _playerSprite;
    private PlayerWeapon _playerWeapon;

    private int _coolDown = 0;
    /*
    public override void Spawned()
    {
        //Runner.GetComponent<NetworkEvents>().OnInput.AddListener(OnInputPlayer);
        _gunSprite = _gun.GetComponent<SpriteRenderer>();
        _playerWeapon = GetComponentInChildren<PlayerWeapon>();

        _playerSprite = _player.GetComponent<SpriteRenderer>();
    }

    public NetworkInput OnInputPlayer(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        data.directionShoot = _actionReferenceRightStick.action.ReadValue<Vector2>();
        input.Set(data);
        return input;
    }
    */
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if (data.directionShoot.magnitude > 0)
            {
                Vector2 direction = data.directionShoot;
                Debug.Log("Gun Pos: " + _gun.position);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                Debug.Log("Gun rotation " + _gun.transform.rotation);


                if (_gun.transform.rotation.z >= 0.7f || _gun.transform.rotation.z <= -0.7f)
                {
                    //_gunSprite.flipX = true;
                    _playerSprite.flipX = true;
                }

                else
                {
                    //_gunSprite.flipX = false;
                    _playerSprite.flipX = false;
                }

                _coolDown++;

                if (_coolDown == 100)
                {
                    Bullet no = _playerWeapon.Shoot();
                    no.SetDirection(_gun.transform.rotation);
                    _coolDown = 0;
                }

            }
        }
    }
}
