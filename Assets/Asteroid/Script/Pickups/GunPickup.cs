using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : Pickup
{

    public override void OnPicked()
    {
        GameManager.GetInstance().pickupSpawner.OnGunPicked();

        base.OnPicked();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            OnPicked();
    }
}
