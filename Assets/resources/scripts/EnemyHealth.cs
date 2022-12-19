using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private EnemyObjectPool enemyObjectPool;
    private float currentHealth;
    private void Start() 
    {
        enemyObjectPool = FindObjectOfType<EnemyObjectPool>();
        maxHealth = enemyObjectPool.wave;
        currentHealth = maxHealth;
        Debug.Log("I am awake!");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Detected a collision!");
        //if (collision.gameObject.tag == "weapon")
        //{
            // Call the function to take damage
            HandleCollision();
            Debug.Log("I took damage!");
        //}
    }

    void HandleCollision()
    {
        // Take damage, and if the damage reduces the health to 0, despawn
        currentHealth--;
        if (currentHealth <= 0f)
        {
            enemyObjectPool.DespawnEnemy(gameObject);
        }
    }
}

