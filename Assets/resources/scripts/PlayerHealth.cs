using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    void Update()
    {
        // Check if the player has taken damage
        if (Input.GetKeyDown(KeyCode.F))
        {
            healthBar.TakeDamage(20);
        }
    }
}