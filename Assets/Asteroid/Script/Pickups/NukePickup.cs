using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickup : Pickup
{
    public override void OnPicked()
    {
        // Add this to a stack of Nukes
        GameManager.GetInstance().pickupSpawner.AddNukePickup();

        base.OnPicked();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            OnPicked();
    }
}
