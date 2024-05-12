using UnityEngine;
using Fusion;

public class PlayerTakeItem : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IItemEffect itemEffect))
        {
            PlayerWeapon weapon = gameObject.GetComponentInChildren<PlayerWeapon>();
            itemEffect.EnableEffect(weapon);
        }
    }
}
