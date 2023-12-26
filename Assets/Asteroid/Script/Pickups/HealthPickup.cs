using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup, IDamageable
{
    [SerializeField] private float healthMin;
    [SerializeField] private float healthMax;


    public override void OnPicked()
    {
        base.OnPicked();
        // Increase Health here
        float health = Random.Range(healthMin, healthMax);

        var player = GameManager.GetInstance().GetPlayer();

        player.health.AddHealth(health);

        Debug.Log($"Added {health} health to player");

    }

    public void GetDamage(float damage)
    {
        // Add health to player (this pickup must be collided with the player or shot at it to be picked up
        OnPicked();
    }
}
