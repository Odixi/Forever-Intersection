using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Ammo,
    Health,
    Gibs
}
public class Pickup : MonoBehaviour
{
    public PickupType PickupType;
    public float Amount;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Player.Instance.OnPickup(PickupType, Amount);
            Destroy(gameObject);
        }
    }
}
