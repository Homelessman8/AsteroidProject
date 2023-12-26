using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


// GameManager class responsible for managing game entities, variables, and game flow
public class GameManager : MonoBehaviour
{
    // Serialized fields for game entities
    [Header("Game Entities")]
    [SerializeField] private GameObject enemyPrefab1;    // Prefab for the first type of enemy
    [SerializeField] private GameObject enemyPrefab2;    // Prefab for the second type of enemy
    [SerializeField] private GameObject enemyPrefab3;    // Prefab for the third type of enemy
    [SerializeField] private GameObject enemyPrefab4;    // Prefab for the fourth type of enemy
    [SerializeField] private Transform[] spawnPositions;  // Array of spawn positions for enemies

    // Serialized fields for game variables
    [Header("Game Variables")]
    [SerializeField] private float enemySpawnRate;       // Rate at which enemies spawn
    [SerializeField] private GameObject playerPrefab;    // Prefab for the player character

    // Events triggered during game start and game over
    public Action OnGameStart;
    public Action OnGameOver;

    // References to other game management components
    public ScoreManager scoreManager;       // Manages game scores
    public PickupSpawner pickupSpawner;     // Spawns pickups

    // References to game entities and variables
    private Player player;                  // Player character
    private GameObject tempEnemy;           // Temporary variable for creating enemies
    private bool isEnemySpawning = false;   // Flag to control enemy spawning
    private bool isPlaying = false;          // Flag to indicate if the game is currently being played

    // Different weapon types with specific attributes
    private Weapon meleeWeapon = new Weapon("Melee", 1, 0);
    private Weapon exploderWeapon = new Weapon("Exploder", 50, 0);
    private Weapon machineGunWeapon = new Weapon("MachineGun", 2, 6);
    private Weapon shooterWeapon = new Weapon("MachineGun", 40, 20);

    // Singleton pattern implementation
    private static GameManager instance;

    // Getter method for the GameManager instance
    public static GameManager GetInstance()
    {
        return instance;
    }

    // Method to set the GameManager as a singleton
    void SetSingleton()
    {
        // Destroy this instance if another instance already exists
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;
    }

    // Awake method called before Start
    void Awake()
    {
        SetSingleton();
    }

    // Method to create a random enemy out of the screen
    void CreateEnemy()
    {
        // Create Melee enemy
        tempEnemy = Instantiate(enemyPrefab1);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon = meleeWeapon;
        tempEnemy.GetComponent<MeleeEnemy>().SetMeleeEnemy(2, 0.25f);

        // Create Exploder enemy
        tempEnemy = Instantiate(enemyPrefab2);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon = exploderWeapon;

        // Create Machine Gun enemy
        tempEnemy = Instantiate(enemyPrefab3);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon = machineGunWeapon;
        tempEnemy.GetComponent<MachineGunEnemy>().SetMachineGunEnemy(5, 4);

        // Create Shooter enemy
        tempEnemy = Instantiate(enemyPrefab4);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon = shooterWeapon;
        tempEnemy.GetComponent<ShooterEnemy>().SetShooterEnemy(7, .25f);
    }

    // Update method called every frame
    private void Update()
    {
        // Testing of random enemy creation when 'X' key is pressed
        if (Input.GetKeyDown(KeyCode.X))
        {
            CreateEnemy();
        }
    }

    // Coroutine for enemy spawning at a defined rate
    IEnumerator EnemySpawner()
    {
        while (isEnemySpawning)
        {
            yield return new WaitForSeconds(1.0f / enemySpawnRate);
            CreateEnemy();
        }
    }

    // Method to notify pickup spawner of an enemy's death
    public void NotifyDealth(Enemy enemy)
    {
        pickupSpawner.SpawnPickup(enemy.transform.position);
    }

    // Getter method for the player character
    public Player GetPlayer() { return player; }

    // Getter method for checking if the game is currently being played
    public bool IsPlaying()
    {
        return isPlaying;
    }

    // Method to start the game
    public void StartGame()
    {
        // Create player character
        player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
        player.OnDeath += StopGame; // Subscribe to player's death event
        isPlaying = true;

        // Invoke the OnGameStart event
        OnGameStart?.Invoke();

        // Start the game after a delay
        StartCoroutine(GameStarter());
    }

    // Coroutine to start the game after a delay
    IEnumerator GameStarter()
    {
        yield return new WaitForSeconds(2.0f);
        isEnemySpawning = true;
        StartCoroutine(EnemySpawner());
    }

    // Method to stop the game
    public void StopGame()
    {
        isEnemySpawning = false; // Stop enemy spawning
        scoreManager.SetHighScore(); // Set the high score

        // Invoke the GameStopper coroutine
        StartCoroutine(GameStopper());
    }

    // Coroutine to stop the game after a delay
    IEnumerator GameStopper()
    {
        isEnemySpawning = false;
        yield return new WaitForSeconds(2.0f);
        isPlaying = false;

        // Delete all enemies
        foreach (Enemy item in FindObjectsOfType(typeof(Enemy)))
        {
            Destroy(item.gameObject);
        }

        // Delete all pickups
        foreach (Pickup item in FindObjectsOfType(typeof(Pickup)))
        {
            Destroy(item.gameObject);
        }

        // Invoke the OnGameOver event
        OnGameOver?.Invoke();
    }
}
