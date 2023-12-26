using UnityEngine;

// ShooterEnemy class, derived from the Enemy class
public class ShooterEnemy : Enemy
{
    // Serialized fields for shooting behavior
    [SerializeField] private float shootingRate;    // Rate at which the enemy shoots
    [SerializeField] private float attackRange;     // Range at which the enemy starts shooting
    [SerializeField] private Bullet bulletPrefab;   // Prefab for the bullet fired by the enemy

    // Internal variables for tracking time, speed, target's Rigidbody2D, and LineRenderer
    private float timer = 0;                        // Timer for shooting rate control
    private float setSpeed = 0;                     // Initial speed of the enemy
    private Rigidbody2D targetRB;                   // Rigidbody2D of the target (player)
    private LineRenderer line;                      // LineRenderer for visualizing the shooting direction

    // Override the Start method of the base class (Enemy)
    protected override void Start()
    {
        base.Start(); // Call the Start method of the base class (Enemy)
        health = new Health(1, 0, 1); // Initialize enemy health
        setSpeed = speed; // Set the initial speed
        line = GetComponent<LineRenderer>(); // Get the LineRenderer component from the enemy

        // If a target is assigned, get its Rigidbody2D
        if (target)
            targetRB = target.GetComponent<Rigidbody2D>();
    }

    // Override the Update method of the base class (Enemy)
    protected override void Update()
    {
        // Predict movement towards the target
        if (target != null)
        {
            Move(target.position);
        }
        else
        {
            Move(speed);
        }

        // If no target is available, return
        if (target == null)
            return;

        // If the target is within attack range, enable LineRenderer and shoot
        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            speed = 0; // Stop movement
            line.enabled = true; // Enable LineRenderer
            line.SetPosition(0, transform.position); // Set LineRenderer start position
            line.SetPosition(1, Vector2.Lerp(target.position, (Vector3)targetRB.velocity.normalized * 1.0f, 0.05f)); // Set LineRenderer end position

            // Call the Shoot method
            Shoot();
        }
        else
        {
            // If the target is outside attack range, disable LineRenderer and resume movement
            line.enabled = false;
            speed = setSpeed;
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

    // Method to set the attributes of the ShooterEnemy (attack range and shooting rate)
    public void SetShooterEnemy(float _attackRange, float _shootingRate)
    {
        attackRange = _attackRange;
        shootingRate = _shootingRate;
    }
}