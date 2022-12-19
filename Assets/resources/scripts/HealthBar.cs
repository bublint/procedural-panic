using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Slider to display the player's current health
    public Slider healthBar;

    // Maximum health of the player
    public float maxHealth = 100f;

    // Current health of the player
    private float currentHealth;

    void Start()
    {
        // Set the initial health of the player
        currentHealth = maxHealth;

        // Set the value of the health bar to the current health
        healthBar.value = currentHealth;
    }

    //void Update()
    //{
    //    // Check if the player has taken damage
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        TakeDamage(20);
    //    }
    //}

    // Function to subtract damage from the player's current health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Clamp the current health between 0 and the maximum health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the value of the health bar to reflect the current health
        healthBar.value = currentHealth;

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            // Trigger game over event or some other appropriate action
        }
    }
}