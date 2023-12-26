using UnityEngine;
public class ExploderEnemy : Enemy
{
    // Internal variable for tracking the initial speed
    private float setSpeed = 0; // Initial speed of the enemy

    // Override the Start method of the base class (Enemy)
    protected override void Start()
    {
        base.Start(); // Call the Start method of the base class (Enemy)
        health = new Health(1, 0, 1); // Initialize enemy health
        setSpeed = speed; // Set the initial speed
    }

    // Override the Update method of the base class (Enemy)
    protected override void Update()
    {
        base.Update(); // Call the Update method of the base class (Enemy)

        // If no target is available, return
        if (target == null)
            return;

        // If the target is very close, stop movement and initiate the attack
        if (Vector2.Distance(transform.position, target.position) < 0.01f)
        {
            speed = 0; // Stop movement
            Attack(0); // Call the Attack method with a specified interval
        }
        else
        {
            speed = setSpeed; // Resume movement
        }
    }

    // Override the Attack method from the base class (Enemy)
    public override void Attack(float interval)
    {
        // Damage the target and destroy the exploder enemy
        target.GetComponent<IDamageable>().GetDamage(weapon.GetDamage());
        Destroy(gameObject);
    }
}
