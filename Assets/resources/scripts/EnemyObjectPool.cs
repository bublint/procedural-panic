// The EnemyObjectPool script manages the instantiation and despawning of enemy game objects,
// as well as keeping track of the number of enemies slain in each wave and advancing to the next wave
// when the required number of enemies have been defeated. It also generates random positions near the player
// character for enemy spawning.

using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    // The prefab for the enemy game object
    public GameObject enemyPrefab;

    // The list of inactive enemy objects available for reuse
    private List<GameObject> inactiveEnemies;

    // Reference to the waves script which manages on-screen information regarding the wave
    [SerializeField] private Waves waveManager;

    // How many enemies have been slain in the current wave
    public int enemiesSlain;

    // How many enemies should spawn in the wave
    [HideInInspector] public int enemiesToSpawn;

    // Stores a value corresponding to which wave is currently active
    [HideInInspector] public int wave;

    // The maximum distance that the random position can be from the player character
    public float maxDistance = 10f;

    // The minimum distance that the random position can be from the player character
    public float minDistance = 1f;

    // The player character transform
    public Transform playerTransform;
    private void Start() 
    {
        // Set enemies slain to 0
        enemiesSlain = 0;
        // Set the current wave to 1
        wave = 1;
        // Set enemies to spawn at beginning of game
        enemiesToSpawn = 10;
        // Create a number of enemy objects and add them to the object pool
        inactiveEnemies = new List<GameObject>();
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            inactiveEnemies.Add(enemy);
        }
        // Set enemies slain to 0 at beginning of game
        enemiesSlain = 0;
    }
   
    // Spawn an enemy near the player character
    public void SpawnEnemy(Vector3 spawnPosition)
    {
        GameObject enemy;

        // Check if there are any inactive enemy objects available
        if (inactiveEnemies.Count > 0)
        {
            // Get the first inactive enemy in the list
            enemy = inactiveEnemies[0];

            // Remove the enemy from the list
            inactiveEnemies.RemoveAt(0);
        }
        else
        {
            // If there are no inactive enemies available, create a new one
            enemy = Instantiate(enemyPrefab);
        }

        // Set the enemy's position and other properties as needed
        enemy.transform.position = spawnPosition;
        enemy.SetActive(true);
    }

    // Despawn an enemy and add it back to the object pool
    public void DespawnEnemy(GameObject enemy)
    {
        enemiesSlain++;
        enemy.SetActive(false);
        inactiveEnemies.Add(enemy);
        TestForWaveClear();
    }

    // Check to see if the requirements have been met to advance to the next wave
    public void TestForWaveClear()
    {
        if (enemiesSlain >= enemiesToSpawn)
        {
            wave++;
            enemiesSlain = 0;
            waveManager.StartWave();
        }
    }

    // Generate a random position near the player character
    public Vector3 GenerateRandomPosition()
    {
        // Generate a random direction
        Vector3 randomDirection = Random.insideUnitSphere;

        // Multiply the random direction by a random distance between the minimum and maximum distances
        float randomDistance = Random.Range(minDistance, maxDistance);
        randomDirection *= randomDistance;

        // Add the random vector to the player character's position to get a random position near the player
        Vector3 randomPosition = playerTransform.position + randomDirection;

        return randomPosition;
    }

    // Spawn a new wave with a certain number of enemies 
    public void SpawnWave()
    {
        enemiesToSpawn = enemiesToSpawn + wave;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy(GenerateRandomPosition());
        }
    }
}