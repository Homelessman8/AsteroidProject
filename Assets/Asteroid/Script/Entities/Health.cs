using UnityEngine;
using System;
public class Health
{
    private float currentHealth;
    private float maxHealth;
    private float healthRegenRate;

    public Action<float> OnHealthUpdate;

    public float GetHealth()
    {
        return currentHealth;
    }

    public void SetHealth(float value)
    {
        if(value > maxHealth || value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), $"Valid range for health is between 0 and {maxHealth}");

        currentHealth = value;
    }

    public Health(float _maxHealth, float _healthRegenRate, float _currentHealth=100)
    {
        currentHealth = _currentHealth;
        maxHealth = _maxHealth;
        healthRegenRate = _healthRegenRate;

        OnHealthUpdate?.Invoke(currentHealth);
    }

    public Health(float _maxHealth)
    {
        maxHealth = _maxHealth;
    }

    //public void RegenHealth()
    //{
    //    AddHealth(healthRegenRate * Time.deltaTime);
    //}

    public Health()
    {}

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + value);
        OnHealthUpdate?.Invoke(currentHealth);
    }

    public void DeductHealth(float value)
    {
        currentHealth = Mathf.Max(0, currentHealth - value);
        OnHealthUpdate?.Invoke(currentHealth);
    }
}
