using UnityEngine;

public class MachineGunEnemy : Enemy
{
    // Serialized fields for machine gun attack behavior
    [SerializeField] private float shootingRate;  // Rate at which the enemy shoots
    [SerializeField] private float attackRange;   // Range at which the enemy starts shooting
    [SerializeField] private Bullet bulletPrefab; // Prefab for the bullet fired by the enemy

    // Internal variables for tracking time, speed, and initial speed
    private float timer = 0;  // Timer for shooting rate control
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

        // If the target is within attack range, stop movement and initiate shooting
        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            speed = 0; // Stop movement
            Shoot(); // Call the Shoot method to initiate shooting
        }
        else
        {
            speed = setSpeed; // Resume movement
        }
    }

    // Override the Shoot method from the base class (Enemy)
    public override void Shoot()
    {
        // Control shooting rate using a timer
        if (timer <= (1.0f / shootingRate))
        {
            timer += Time.deltaTime;
        }
        else
        {
            // Reset the timer and perform the shooting action
            timer = 0;
            weapon.Shoot(bulletPrefab, this, "Player");
        }
    }

    // Method to set the attributes of the MachineGunEnemy (attack range and shooting rate)
    public void SetMachineGunEnemy(float _attackRange, float _shootingRate)
    {
        attackRange = _attackRange;
        shootingRate = _shootingRate;
    }
}
