using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Waves : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;
    private Color currentColor;
    [SerializeField] private float fadeInTime = 1f;
    [SerializeField] private float fadeOutTime = 1f;
    public int currentWave = 0;
    [SerializeField] private EnemyObjectPool enemyObjectPool;
    void Start()
    {
        currentWave = 0;
        currentColor = waveText.color;
        currentColor.a = 0f;
        waveText.color = currentColor;
        waveText.SetText("Wave " + currentWave);
        StartWave();
    }
    
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

