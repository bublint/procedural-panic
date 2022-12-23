// Waves is a script that manages the wave system in the game
// It displays the current wave number on the screen and uses an EnemyObjectPool to spawn enemies
// The script has a fade in/out effect for the wave text using coroutines

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Waves : MonoBehaviour
{
    // Reference to the TMP_Text component that displays the wave number on the screen
    [SerializeField] private TMP_Text waveText;

    // Stores the current color of the waveText
    private Color currentColor;

    // The fadeInTime and fadeOutTime fields are the durations of the fade in and fade out effects for the waveText
    [SerializeField] private float fadeInTime = 1f;
    [SerializeField] private float fadeOutTime = 1f;

    // Stores a value for the current wave
    public int currentWave = 0;

    // Reference to the EnemyObjectPool script, which is used to spawn enemies
    [SerializeField] private EnemyObjectPool enemyObjectPool;

    // Initialize current wave and current color fields
    // Start the fade in/out function
    // Start the first wave using the SpawnWave function in enemyObjectPool
    void Start()
    {
        currentWave = 0;
        currentColor = waveText.color;
        currentColor.a = 0f;
        waveText.color = currentColor;
        waveText.SetText("Wave " + currentWave);
        StartWave();
    }
    
    // Increments the current wave number and updates the text of the waveText
    // Starts the fade in/out effect using the FadeInOut function
    // Starts the wave using the SpawnWave function in the enemyObjectPool
    public void StartWave()
    {
        currentWave++; // Increment the current wave number
        waveText.SetText("Wave " + currentWave);
        FadeInOut();
        enemyObjectPool.SpawnWave();
    }

    void FadeInOut()
    {
        StartCoroutine(FadeInCoroutine());
    }
    // Gradually increase the alpha value of the currentColor field over the duration of the fadeInTime.
    IEnumerator FadeInCoroutine()
    {
        while (currentColor.a < 1f)
        {
            //currentColor.a += fadeInTime * Time.deltaTime;
            currentColor.a += (Time.deltaTime / fadeInTime);
            waveText.color = currentColor;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeOutCoroutine());
    }
    // Gradually decrease the alpha value of the currentColor field over the duration of the fadeOutTime
    IEnumerator FadeOutCoroutine()
    {
        while (waveText.alpha > 0f)
        {
            //currentColor.a -= fadeOutTime * Time.deltaTime;
            currentColor.a -= (Time.deltaTime / fadeOutTime);
            waveText.color = currentColor;
        }
        yield return null;
    }
}

