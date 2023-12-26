using UnityEngine;

// MeleeEnemy class, derived from the Enemy class
public class MeleeEnemy : Enemy
{
    // Serialized fields for melee attack behavior
    [SerializeField] private float attackRange;  // Range at which the enemy starts melee attack
    [SerializeField] private float attackTime = 0; // Time interval between consecutive attacks

    // Internal variables for tracking time, speed, and initial speed
    private float timer = 0; // Timer for attack time interval control
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

        // If the target is within attack range, stop movement and initiate attack
        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            speed = 0; // Stop movement
            Attack(attackTime); // Call the Attack method with the specified attack time interval
        }
        else
        {
            speed = setSpeed; // Resume movement
        }
    }

    // Override the Attack method from the base class (Enemy)
    public override void Attack(float interval)
    {
        // Control attack time interval using a timer
        if (timer <= interval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            // Reset the timer and perform the attack action
            timer = 0;
            target.GetComponent<IDamageable>().GetDamage(weapon.GetDamage());
        }
    }

    // Method to set the attributes of the MeleeEnemy (attack range and attack time interval)
    public void SetMeleeEnemy(float _attackRange, float _attackTime)
    {
        attackRange = _attackRange;
        attackTime = _attackTime;
    }
}