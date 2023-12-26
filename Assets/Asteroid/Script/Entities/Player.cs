using UnityEngine;
using System;

// Player class inherits from PlayableObject
// PlayableObject is assumed to be a base class for playable characters
public class Player : PlayableObject
{
    // SerializedField allows private variables to be displayed in the Unity Editor
    [SerializeField] private Camera cam;
    [SerializeField] private float speed;

    // Weapon-related properties
    [SerializeField] private float weaponDamage = 1;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private Bullet bulletPrefab;

    // Machine gun properties
    private float gunRate = .1f;
    private float machineGunRate = 10f;
    private float normalGunRate = .1f;
    private float gunTimer = 0;

    // Action invoked on player's death
    public Action OnDeath;

    // Rigidbody2D for player's movement
    private Rigidbody2D playerRB;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Initialize player's health with default values
        health = new Health(100, 0.5f, 50);

        // Get the Rigidbody2D component attached to the player
        playerRB = GetComponent<Rigidbody2D>();

        // Set player's weapon with default values
        weapon = new Weapon("Player Weapon", weaponDamage, bulletSpeed);

        // Get the main camera
        cam = Camera.main;
    }

    // Move function handles player movement
    public override void Move(Vector2 direction, Vector2 target)
    {
        // Set player's velocity based on the input direction and speed
        playerRB.velocity = direction * speed * Time.deltaTime;

        // Convert player's position to screen space
        var playerScreenPos = cam.WorldToScreenPoint(transform.position);

        // Adjust the target coordinates relative to the player's screen position
        target.x -= playerScreenPos.x;
        target.y -= playerScreenPos.y;

        // Calculate the angle for player rotation
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        // Apply rotation to the player
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    private void Update()
    {
        // Uncomment the line below if health regeneration is intended
        // health.RegenHealth();
    }

    // Shoot function handles player shooting
    public override void Shoot()
    {
        // Check if a machine gun pickup is active
        if (GameManager.GetInstance().pickupSpawner.IsGunPicked())
        {
            gunRate = machineGunRate;
        }
        else
        {
            gunRate = normalGunRate;
        }

        // Shoot a bullet if the timer is zero
        if (gunTimer == 0)
            weapon.Shoot(bulletPrefab, this, "Enemy");

        // Increment the timer and reset it if it exceeds the shooting rate
        gunTimer += Time.deltaTime;
        if (gunTimer > (1.0f / gunRate))
        {
            gunTimer = 0;
        }
    }

    // StopShoot function resets the shoot timer
    public void StopShoot()
    {
        gunTimer = 0;
    }

    // Die function handles player's death
    public override void Die()
    {
        // Log a message and invoke the OnDeath event
        Debug.Log("Player died!");
        OnDeath?.Invoke();

        // Destroy the player object
        Destroy(gameObject);
    }

    // Attack function handles special attacks
    public override void Attack(float interval)
    {
        // Use a nuke if available, destroying all enemies in the scene
        if (GameManager.GetInstance().pickupSpawner.ChooseNuke())
        {
            foreach (Enemy enemy in FindObjectsOfType(typeof(Enemy)))
            {
                enemy.Die();
            }
        }
    }

    // GetDamage function deducts player's health when taking damage
    public override void GetDamage(float damage)
    {
        health.DeductHealth(damage);

        // Check if player's health is zero or less and call Die function
        if (health.GetHealth() <= 0)
            Die();
    }
}
