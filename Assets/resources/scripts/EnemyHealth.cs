// EnemyHealth is a script that manages the health of an enemy game object
// It has a maxHealth value that represents the maximum health of the enemy
// It also has a currentHealth value that represents the current health of the enemy
// The script has a function called HandleCollision that reduces the enemy's health by a certain amount when the enemy collides with something
// If the enemy's health is reduced to 0 or below, it will be despawned using the enemyObjectPool

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Stores the max health value of the enemy unit
    [SerializeField] private float maxHealth;

    // The enemyObjectPool field is a reference to the EnemyObjectPool script, which is used to despawn the enemy when its health reaches 0
    [SerializeField] private EnemyObjectPool enemyObjectPool;

    private float currentHealth;
    
    // Find the EnemyObjectPool object in the scene and store it in the enemyObjectPool field
    // Set the maxHealth value to be equal to the current wave in the EnemyObjectPool script
    // Set the currentHealth value to be equal to the maxHealth value
    private void Start() 
    {
        enemyObjectPool = FindObjectOfType<EnemyObjectPool>();
        maxHealth = enemyObjectPool.wave;
        currentHealth = maxHealth;
        Debug.Log("I am awake!");
    }
    
    // Checks if the collision was with an object that has the "weapon" tag, and if so, calls the HandleCollision function
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Detected a collision!");
        //if (collision.gameObject.tag == "weapon")
        //{
            // Call the function to take damage
            // HandleCollision(1f);
            Debug.Log("I took damage!");
        //}
    }

    // Reduce the enemy's currentHealth value by the damage parameter
    // If the enemy's currentHealth is less than or equal to 0, the enemy is despawned using the DespawnEnemy function in the enemyObjectPool
    public void HandleCollision(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            enemyObjectPool.DespawnEnemy(gameObject);
        }
    }
}

