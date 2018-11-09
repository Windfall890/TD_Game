using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPrefab;
    public Transform spawnPoint;

    public Text waveCountDownText;
    public float waveDelayTime = 5.5f;
    public float spawnDelay = 0.5f;
    public int waveIndex = 0;

    private float countDown = 2f;
    

    public void Update()
    {
        if( countDown <= 0)
        {
            StartCoroutine(SpawnWave());
            countDown = waveDelayTime;
        }
        countDown -= Time.deltaTime;

        waveCountDownText.text = Mathf.Round(countDown).ToString();
    }

    public IEnumerator SpawnWave ()
    {
        Debug.Log("WAVE INCOMING!");

        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(VariableDelay());
        }
    }

    private float VariableDelay()
    {
        return spawnDelay + Random.Range(-0.2f, 0.2f);
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
