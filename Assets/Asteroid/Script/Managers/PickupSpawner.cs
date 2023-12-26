using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField]private PickupSpawn[] pickups;

    [Range(0,1)]
    [SerializeField]private float pickupProbability;

    [SerializeField] private int maxNukes = 3;

    public System.Action OnNukePickup;
    public System.Action OnNukeChosen;
    public System.Action<bool, float> OnGunTimerUpdated;

    List<Pickup> pickupPool = new List<Pickup>();
    Pickup chosenPickup;

    private int pickedNukes=0;
    private bool isGunPicked = false;

    private float gunPickedTimer = 0;
    private float gunPickedMaxTime = 5;

    private void Start()
    {
        // Populate a pool of pickups
        foreach (PickupSpawn spawn in pickups)
        {
            for (int i = 0; i < spawn.spawnWeight; i++)
            {
                pickupPool.Add(spawn.pickup);
            }
        }
    }

    public void SpawnPickup(Vector2 position)
    {
        if (pickupPool.Count <= 0)
            return;

        if (Random.Range(0.0f, 1.0f) < pickupProbability)
        {
            chosenPickup = pickupPool[Random.Range(0, pickupPool.Count)];
            Instantiate(chosenPickup, position, Quaternion.identity);
        }
    }

    public void AddNukePickup()
    {
        if (pickedNukes < maxNukes)
        {
            pickedNukes++;
            OnNukePickup?.Invoke();
        }
    }

    public bool ChooseNuke()
    {
        if (pickedNukes > 0)
        {
            pickedNukes = Mathf.Max(0, pickedNukes - 1);
            pickedNukes--;
            OnNukeChosen?.Invoke();
            return true;

        }

        return false;
    }

    private void Update()
    {
        ManageGunPickup();
    }

    void ManageGunPickup()
    {
        gunPickedTimer += Time.deltaTime;

        if(gunPickedTimer >= gunPickedMaxTime)
            isGunPicked = false;

        OnGunTimerUpdated?.Invoke(isGunPicked, gunPickedTimer / gunPickedMaxTime);

    }

    public void OnGunPicked()
    {
        gunPickedTimer = 0;
        isGunPicked = true;
    }

    public bool IsGunPicked()
    {
        return isGunPicked;
    }

    public void ClearPickups()
    {
        pickedNukes = 0;
        isGunPicked = false;

        gunPickedTimer = 0;
    }

    public int GetMaxNukes()
    { 
        return maxNukes; 
    }


}

[System.Serializable]
public struct PickupSpawn
{
    public Pickup pickup;
    public int spawnWeight;
}
