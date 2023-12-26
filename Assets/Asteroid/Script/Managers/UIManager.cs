using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject LblGameOver;
    [SerializeField] private TMP_Text txtMenuHighScore;

    [Header("Gameplay")]
    [SerializeField] private TMP_Text txtHealth;
    [SerializeField] private TMP_Text txtScore;
    [SerializeField] private TMP_Text txtHighScore;

    [Header("Nukes")]
    [SerializeField] private GameObject nukeUIPrefab;
    [SerializeField] private Transform nukeUIHolder;

    [Header("Machine Gun")]
    [SerializeField] private Image gunTimerUI;
    [SerializeField] private GameObject gunTimerUIHolder;

    private Player player;
    private ScoreManager scoreManager;
    private int currentNuke;

    // Keep track of instantiated nukeUIPrefabs
    private int instantiatedNukes = 0;

    private void Start()
    {
        scoreManager = GameManager.GetInstance().scoreManager;

        GameManager.GetInstance().OnGameStart += GameStarted;
        GameManager.GetInstance().OnGameOver += GameOver;

        GameManager.GetInstance().pickupSpawner.OnNukePickup += OnNukePicked;
        GameManager.GetInstance().pickupSpawner.OnNukeChosen += OnNukeUsed;

        GameManager.GetInstance().pickupSpawner.OnGunTimerUpdated += UpdateGunTimer;
    }

    public void UpdateHealth(float currentHealth)
    {
        txtHealth.SetText(Mathf.Floor(currentHealth).ToString());
    }

    public void UpdateScore()
    {
        txtScore.SetText(scoreManager.GetScore().ToString());
    }

    public void UpdateHighScore()
    {
        txtHighScore.SetText(scoreManager.GetHighScore().ToString());
        txtMenuHighScore.SetText($"High Score : {scoreManager.GetHighScore()}");
    }

    public void GameStarted()
    {
        ClearUI();
        player = GameManager.GetInstance().GetPlayer();
        player.health.OnHealthUpdate += UpdateHealth;

        MenuCanvas.SetActive(false);
    }

    public void GameOver()
    {
        player.health.OnHealthUpdate -= UpdateHealth;

        LblGameOver.SetActive(true);
        MenuCanvas.SetActive(true);
    }


    // OnNukePicked is called when a nuke is picked up
    public void OnNukePicked()
    {
        // Activate the UI representation of the current nuke
        nukeUIHolder.GetChild(currentNuke).gameObject.SetActive(true);

        // Increment the current nuke count
        currentNuke++;
    }

    // OnNukeUsed is called when a nuke is used
    public void OnNukeUsed()
    {
        // Decrement the current nuke count
        currentNuke--;

        // Deactivate the UI representation of the current nuke
        nukeUIHolder.GetChild(currentNuke).gameObject.SetActive(false);
    }


    // UpdateGunTimer updates the UI elements related to the player's gun timer
    public void UpdateGunTimer(bool active, float gunTimerFraction)
    {
        // Set the visibility of the gun timer UI holder
        gunTimerUIHolder.SetActive(active);

        // If the gun timer UI is active, set its position to the player's position
        if (active)
            gunTimerUIHolder.transform.position = player.transform.position;

        // Update the fill amount of the gun timer UI based on the provided fraction
        gunTimerUI.fillAmount = (1 - gunTimerFraction);
    }

    public void ClearUI()
    {
        txtScore.SetText("0");

        currentNuke = 0;


        // Clear nukes
        foreach (Transform nukeUI in nukeUIHolder)
            Destroy(nukeUI.gameObject);

        // Add nukes
        for (int i = 0; i < GameManager.GetInstance().pickupSpawner.GetMaxNukes(); i++)
        {
            var nuke = Instantiate(nukeUIPrefab, nukeUIHolder);
            nuke.SetActive(false);
        }


    }
}
